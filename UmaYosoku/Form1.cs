using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmaYosoku
{
    public partial class Form1 : Form
    {
        // =========================
        // PARALLAX SETTINGS
        // =========================

        private const int BgMargin = 30;           // extra background size so edges never show
        private const float BgStrength = 20f;      // background movement (keep < BgMargin)
        private const float PanelStrength = 12f;   // panel movement
        private const float Smoothing = 0.12f;     // 0.05 = floaty, 0.3 = snappy

        // =========================
        // INTRO SETTINGS (milliseconds)
        // =========================

        private const int WindowFadeInMs = 500;    // window fades in to white
        private const int LogoFadeInMs = 700;      // logo fades in
        private const int AfterVoiceDelayMs = 250; // pause after the voice ends
        private const int UiFadeInMs = 900;        // white fades away to reveal UI

        // =========================
        // EXIT SETTINGS (milliseconds)
        // =========================

        private const int MusicFadeOutMs = 1000;   // umapyoi goes 100% -> 0% over this time, THEN the exit sound plays

        private PictureBox pictureBox;
        private Image animatedBackground;   // the real animated GIF
        private Bitmap staticBackground;    // first frame only, used during the intro
        private MenuPanel menuPanel;
        private System.Windows.Forms.Timer parallaxTimer;

        private SplashOverlay splash;
        private Bitmap uiSnapshot;
        private bool introDone = false;

        private System.Windows.Forms.Button exitBtn;
        private bool exiting = false;

        private float curBgX, curBgY, curPanelX, curPanelY;
        private float lastDepthX = float.NaN, lastDepthY = float.NaN;

        // =========================
        // ANTI-FLICKER
        // Composited painting double-buffers the form AND all its child controls
        // =========================

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public Form1()
        {
            InitializeComponent();

            // =========================
            // FORM SETTINGS
            // =========================

            this.Text = "UmaYosoku";
            this.Size = new Size(400, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.DoubleBuffered = true;

            // =========================
            // ICON
            // =========================

            this.Icon = new Icon(
                new MemoryStream(
                    Properties.Resources.oguri_icon
                )
            );

            // =========================
            // BACKGROUND
            // (oversized and NOT docked, so it can move)
            // During the intro it shows a still first frame, so the splash
            // crossfade matches perfectly. The GIF starts playing right
            // after the splash is gone.
            // =========================

            animatedBackground = Properties.Resources.oguri_cap;
            staticBackground = new Bitmap(animatedBackground); // copy of frame 0

            pictureBox = new PictureBox();

            pictureBox.Image = staticBackground;

            pictureBox.SizeMode =
                PictureBoxSizeMode.StretchImage;

            pictureBox.Size = new Size(
                this.ClientSize.Width + BgMargin * 2,
                this.ClientSize.Height + BgMargin * 2
            );

            pictureBox.Location =
                new Point(-BgMargin, -BgMargin);

            this.Controls.Add(pictureBox);

            // =========================
            // MENU PANEL
            // =========================

            menuPanel =
                new MenuPanel();

            menuPanel.Size =
                new Size(320, 600);

            pictureBox.Controls.Add(menuPanel);

            menuPanel.BringToFront();

            // =========================
            // EXIT BUTTON (bottom center of the panel)
            // (fully qualified because the UmaYosoku.Button namespace exists)
            // =========================

            exitBtn = new System.Windows.Forms.Button();
            exitBtn.Text = "Exit";
            exitBtn.Size = new Size(80, 32);
            exitBtn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            exitBtn.ForeColor = Color.White;
            exitBtn.BackColor = Color.Transparent;
            exitBtn.FlatStyle = FlatStyle.Flat;
            exitBtn.FlatAppearance.BorderColor = Color.White;
            exitBtn.FlatAppearance.BorderSize = 1;
            exitBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            exitBtn.FlatAppearance.MouseDownBackColor = Color.FromArgb(90, 90, 90);
            exitBtn.Cursor = Cursors.Hand;
            exitBtn.Location = new Point(
                (menuPanel.Width - exitBtn.Width) / 2,
                menuPanel.Height - exitBtn.Height - 50   // change 50 to move it up/down
            );
            exitBtn.Click += ExitButton_Click;
            menuPanel.Controls.Add(exitBtn);
            exitBtn.BringToFront();

            UpdatePositions();

            // =========================
            // SPLASH OVERLAY (on top of everything)
            // =========================

            splash = new SplashOverlay
            {
                Dock = DockStyle.Fill,
                Logo = Properties.Resources.ura
            };
            this.Controls.Add(splash);
            splash.BringToFront();

            this.Opacity = 0; // window itself fades in during the intro

            // =========================
            // SOUNDS
            // (intro voice, exit sound and background music all use NAudio;
            //  the music lives in the shared Bgm class)
            // =========================

            // =========================
            // PARALLAX TIMER (starts after the intro)
            // =========================

            parallaxTimer = new System.Windows.Forms.Timer();
            parallaxTimer.Interval = 16; // ~60 FPS
            parallaxTimer.Tick += ParallaxTimer_Tick;
        }

        // Shows why a sound could not be played (so we can see the real reason)
        private void ShowAudioError(string what, Exception ex)
        {
            MessageBox.Show(
                this,
                "Could not play the " + what + " audio:\n\n" + ex.Message,
                "UmaYosoku audio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        // =========================
        // INTRO SEQUENCE
        // =========================

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await PlayIntro();
        }

        private async Task PlayIntro()
        {
            // 0) Take a picture of the finished UI (the splash is covering it).
            //    The white layer fades over this picture, which avoids flicker.
            uiSnapshot = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.DrawToBitmap(
                uiSnapshot,
                new Rectangle(Point.Empty, pictureBox.Size));
            splash.Snapshot = uiSnapshot;
            splash.SnapshotLocation = pictureBox.Location;

            // 1) Window fades in to a white screen
            await Animate(WindowFadeInMs, t => this.Opacity = t);

            // 2) Logo fades in
            await Animate(LogoFadeInMs, t =>
            {
                splash.LogoAlpha = t;
                splash.Invalidate();
            });

            // 3) Voice says the app name, wait until it finishes
            try
            {
                using (SfxClip voice = Audio.OpenClip(Properties.Resources.oguri_intro))
                {
                    voice.Play();
                    await voice.Finished;
                }
            }
            catch (Exception ex)
            {
                ShowAudioError("intro", ex);
            }

            await Task.Delay(AfterVoiceDelayMs);

            // 4) Fade the white + logo away to reveal the UI, start BGM
            try
            {
                Bgm.Load();
                Bgm.Play();
            }
            catch (Exception ex)
            {
                ShowAudioError("background music", ex);
            }

            await Animate(UiFadeInMs, t =>
            {
                splash.WhiteAlpha = 1f - t;
                splash.LogoAlpha = 1f - t;
                splash.Invalidate();
            });

            this.Controls.Remove(splash);
            splash.Snapshot = null;
            splash.Dispose();
            splash = null;

            uiSnapshot.Dispose();
            uiSnapshot = null;

            // 5) Splash is gone: switch to the real animated GIF.
            //    It starts from frame 0, which matches the still frame we showed.
            pictureBox.Image = animatedBackground;
            staticBackground.Dispose();
            staticBackground = null;

            introDone = true;
            parallaxTimer.Start();
        }

        // Runs a smooth 0..1 animation over 'ms' milliseconds
        private async Task Animate(int ms, Action<float> step)
        {
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < ms)
            {
                float t = sw.ElapsedMilliseconds / (float)ms;
                step(t * t * (3f - 2f * t)); // smoothstep easing
                await Task.Delay(16);
            }
            step(1f);
        }

        // =========================
        // PARALLAX UPDATE
        // =========================

        private void ParallaxTimer_Tick(object sender, EventArgs e)
        {
            // Cursor position relative to the form
            // (works even when hovering over the panel, buttons, or title)
            Point mouse = this.PointToClient(Cursor.Position);

            float halfW = this.ClientSize.Width / 2f;
            float halfH = this.ClientSize.Height / 2f;

            // Normalize to -1..1 and clamp
            float nx = Math.Max(-1f, Math.Min(1f, (mouse.X - halfW) / halfW));
            float ny = Math.Max(-1f, Math.Min(1f, (mouse.Y - halfH) / halfH));

            // Background moves opposite (far away),
            // panel moves with the cursor (closer)
            float targetBgX = -nx * BgStrength;
            float targetBgY = -ny * BgStrength;
            float targetPanelX = nx * PanelStrength;
            float targetPanelY = ny * PanelStrength;

            // Smooth (lerp)
            curBgX += (targetBgX - curBgX) * Smoothing;
            curBgY += (targetBgY - curBgY) * Smoothing;
            curPanelX += (targetPanelX - curPanelX) * Smoothing;
            curPanelY += (targetPanelY - curPanelY) * Smoothing;

            UpdatePositions();
        }

        private void UpdatePositions()
        {
            // Move background
            pictureBox.Location = new Point(
                (int)Math.Round(-BgMargin + curBgX),
                (int)Math.Round(-BgMargin + curBgY)
            );

            // Panel position on the FORM (centered + its own offset)
            int panelFormX =
                (this.ClientSize.Width - menuPanel.Width) / 2
                + (int)Math.Round(curPanelX);

            int panelFormY =
                (this.ClientSize.Height - menuPanel.Height) / 2
                + (int)Math.Round(curPanelY);

            // Convert to PictureBox coordinates (it is the parent)
            menuPanel.Location = new Point(
                panelFormX - pictureBox.Left,
                panelFormY - pictureBox.Top
            );

            // Title and buttons float a bit more for extra depth.
            // Only update when the value really changed, so we don't repaint for nothing.
            if (float.IsNaN(lastDepthX)
                || Math.Abs(curPanelX - lastDepthX) > 0.05f
                || Math.Abs(curPanelY - lastDepthY) > 0.05f)
            {
                lastDepthX = curPanelX;
                lastDepthY = curPanelY;
                menuPanel.SetDepthOffset(curPanelX, curPanelY);
            }
        }

        // =========================
        // PAUSE PARALLAX WHEN FORM IS HIDDEN
        // (you hide Form1 when opening PickARacePanel)
        // The music is NOT touched here, so it keeps playing.
        // =========================

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (parallaxTimer == null || !introDone)
            {
                return; // don't touch anything while the intro is running
            }

            if (this.Visible)
            {
                parallaxTimer.Start();
            }
            else
            {
                parallaxTimer.Stop();
            }
        }

        // =========================
        // EXIT: music fades 100% -> 0%, THEN the exit sound plays
        // and the screen fades to black (same length as the exit sound)
        // =========================

        private async void ExitButton_Click(object sender, EventArgs e)
        {
            if (exiting) return;   // ignore double clicks
            exiting = true;
            exitBtn.Enabled = false;

            // Load the exit sound; the fade length = the length of the sound
            SfxClip exitClip = null;
            int ms = 1500; // fallback if the length can't be read

            try
            {
                exitClip = Audio.OpenClip(Properties.Resources.rice_shower_exit);
                if (exitClip.DurationMs >= 300) ms = exitClip.DurationMs;
            }
            catch (Exception ex)
            {
                ShowAudioError("exit", ex);
            }

            // Black window that sits exactly on top of this form
            Form fade = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                Bounds = this.Bounds,
                BackColor = Color.Black,
                ShowInTaskbar = false,
                Opacity = 0
            };
            fade.Show(this);

            // 1) The music fades smoothly from 100% to 0%
            await FadeOutMusic();

            // 2) After that, the exit sound plays and the screen fades to black
            exitClip?.Play();

            await Animate(ms, t =>
            {
                fade.Opacity = t;
            });

            // Wait until the exit sound has really finished (max 1 extra second)
            if (exitClip != null)
            {
                await Task.WhenAny(exitClip.Finished, Task.Delay(1000));
                exitClip.Dispose();
            }

            fade.Dispose();
            Application.Exit();
        }

        // Fades the music from 100% to 0%, then stops it.
        // Our ears hear volume on a curve, not a straight line: a straight
        // 100->0 sounds like it stays loud and then drops at the end.
        // Squaring the value makes the drop sound even from start to finish.
        private async Task FadeOutMusic()
        {
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < MusicFadeOutMs)
            {
                float t = sw.ElapsedMilliseconds / (float)MusicFadeOutMs;
                float level = 1f - t;                  // 1 -> 0
                Bgm.SetVolume((int)(1000 * level * level));
                await Task.Delay(16);
            }

            Bgm.SetVolume(0);
            Bgm.Stop();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            parallaxTimer?.Stop();
            parallaxTimer?.Dispose();
            Bgm.Stop();
            uiSnapshot?.Dispose();
            staticBackground?.Dispose();
            animatedBackground?.Dispose();
            base.OnFormClosed(e);
        }
    }
}