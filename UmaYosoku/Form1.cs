using System;
<<<<<<< HEAD
using System.Drawing;
using System.IO;
=======
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
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

<<<<<<< HEAD
        private PictureBox pictureBox;
        private MenuPanel menuPanel;
        private System.Windows.Forms.Timer parallaxTimer;

        private float curBgX, curBgY, curPanelX, curPanelY;
=======
        // =========================
        // INTRO SETTINGS (milliseconds)
        // =========================

        private const int WindowFadeInMs = 500;    // window fades in to white
        private const int LogoFadeInMs = 700;      // logo fades in
        private const int AfterVoiceDelayMs = 250; // pause after the voice ends
        private const int UiFadeInMs = 900;        // white fades away to reveal UI

        private PictureBox pictureBox;
        private Image animatedBackground;   // the real animated GIF
        private Bitmap staticBackground;    // first frame only, used during the intro
        private MenuPanel menuPanel;
        private System.Windows.Forms.Timer parallaxTimer;

        private SplashOverlay splash;
        private Bitmap uiSnapshot;
        private SoundPlayer voicePlayer;
        private SoundPlayer bgmPlayer;
        private bool introDone = false;

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
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9

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
<<<<<<< HEAD
            // =========================

            pictureBox = new PictureBox();

            pictureBox.Image =
                Properties.Resources.oguri_cap;
=======
            // During the intro it shows a still first frame, so the splash
            // crossfade matches perfectly. The GIF starts playing right
            // after the splash is gone.
            // =========================

            animatedBackground = Properties.Resources.oguri_cap;
            staticBackground = new Bitmap(animatedBackground); // copy of frame 0

            pictureBox = new PictureBox();

            pictureBox.Image = staticBackground;
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9

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

            UpdatePositions();

            // =========================
<<<<<<< HEAD
            // PARALLAX TIMER
=======
            // SPLASH OVERLAY (on top of everything)
            // =========================

            splash = new SplashOverlay
            {
                Dock = DockStyle.Fill,
                Logo = Properties.Resources.oguri_cap_chibi
            };
            this.Controls.Add(splash);
            splash.BringToFront();

            this.Opacity = 0; // window itself fades in during the intro

            // =========================
            // SOUNDS
            // =========================

            voicePlayer = new SoundPlayer(Properties.Resources.intro);
            bgmPlayer = new SoundPlayer(Properties.Resources.umapyoi);

            // =========================
            // PARALLAX TIMER (starts after the intro)
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
            // =========================

            parallaxTimer = new System.Windows.Forms.Timer();
            parallaxTimer.Interval = 16; // ~60 FPS
            parallaxTimer.Tick += ParallaxTimer_Tick;
<<<<<<< HEAD
            parallaxTimer.Start();

            // =========================
            // DASHBOARD PANEL
            // =========================

=======
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
            voicePlayer.Load();
            await Task.Run(() => voicePlayer.PlaySync());
            await Task.Delay(AfterVoiceDelayMs);

            // 4) Fade the white + logo away to reveal the UI, start BGM
            bgmPlayer.Load();
            bgmPlayer.PlayLooping();

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
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
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

<<<<<<< HEAD
            // Title and buttons float a bit more for extra depth
            menuPanel.SetDepthOffset(curPanelX, curPanelY);
        }

        // =========================
        // PAUSE TIMER WHEN FORM IS HIDDEN
=======
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
        // PAUSE TIMER + BGM WHEN FORM IS HIDDEN
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
        // (you hide Form1 when opening PickARacePanel)
        // =========================

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

<<<<<<< HEAD
            if (parallaxTimer == null)
            {
                return;
=======
            if (parallaxTimer == null || !introDone)
            {
                return; // don't touch anything while the intro is running
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
            }

            if (this.Visible)
            {
                parallaxTimer.Start();
<<<<<<< HEAD
=======
                bgmPlayer.PlayLooping();
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
            }
            else
            {
                parallaxTimer.Stop();
<<<<<<< HEAD
=======
                bgmPlayer.Stop();
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            parallaxTimer?.Stop();
            parallaxTimer?.Dispose();
<<<<<<< HEAD
=======
            voicePlayer?.Stop();
            bgmPlayer?.Stop();
            uiSnapshot?.Dispose();
            staticBackground?.Dispose();
            animatedBackground?.Dispose();
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
            base.OnFormClosed(e);
        }
    }
}