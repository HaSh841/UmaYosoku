using System;
using System.Drawing;
using System.IO;
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

        private PictureBox pictureBox;
        private MenuPanel menuPanel;
        private System.Windows.Forms.Timer parallaxTimer;

        private float curBgX, curBgY, curPanelX, curPanelY;

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
            // =========================

            pictureBox = new PictureBox();

            pictureBox.Image =
                Properties.Resources.oguri_cap;

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
            // PARALLAX TIMER
            // =========================

            parallaxTimer = new System.Windows.Forms.Timer();
            parallaxTimer.Interval = 16; // ~60 FPS
            parallaxTimer.Tick += ParallaxTimer_Tick;
            parallaxTimer.Start();

            // =========================
            // DASHBOARD PANEL
            // =========================

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

            // Title and buttons float a bit more for extra depth
            menuPanel.SetDepthOffset(curPanelX, curPanelY);
        }

        // =========================
        // PAUSE TIMER WHEN FORM IS HIDDEN
        // (you hide Form1 when opening PickARacePanel)
        // =========================

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (parallaxTimer == null)
            {
                return;
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            parallaxTimer?.Stop();
            parallaxTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}