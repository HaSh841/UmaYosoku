using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
using UmaYosoku.PANELS;
using UmaYosoku.Button;

namespace UmaYosoku.UMA_DATA
{
    public partial class MejiroMcQueenData : Form
    {
        // =========================
        // PARALLAX SETTINGS
        // =========================

        private const int BgMargin = 40;
        private const float BgStrength = 25f;      // keep < BgMargin
        private const float PanelStrength = 12f;
        private const float Smoothing = 0.12f;

        private PictureBox picBox;
        private RaceOverlayPanel overlay;
        private System.Windows.Forms.Timer parallaxTimer;
        private float curBgX, curBgY, curPanelX, curPanelY;

        private SoundPlayer sfx;

        private PickARacePanel pick15;

        public MejiroMcQueenData(PickARacePanel pick15)
        {




            // =========================
            // STORE PREVIOUS FORM
            // =========================

            this.pick15 = pick15;

            // =========================
            // FORM SETTINGS
            // =========================

            this.AutoScaleMode = AutoScaleMode.None;

            this.DoubleBuffered = true;

            this.Text = "UmaYosoku";

            this.ClientSize =
                new Size(500, 700);

            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.MaximizeBox = false;

            this.MinimizeBox = true;

            this.FormBorderStyle =
                FormBorderStyle.FixedSingle;

            this.Icon = new Icon(
                new MemoryStream(
                    Properties.Resources.oguri_icon
                )
            );

            // =========================
            // BACK BUTTON SFX
            // =========================

            sfx = new SoundPlayer(
                Properties.Resources.sfx1
            );

            // =========================
            // BACKGROUND
            // (oversized and NOT docked, so it can move)
            // =========================

            picBox =
                new PictureBox();

            picBox.Image =
                Properties.Resources.MejiroMcQueenGif;

            picBox.SizeMode =
                PictureBoxSizeMode.StretchImage;

            picBox.Size = new Size(
                this.ClientSize.Width + BgMargin * 2,
                this.ClientSize.Height + BgMargin * 2
            );

            picBox.Location =
                new Point(-BgMargin, -BgMargin);

            EnableDoubleBuffer(picBox);

            this.Controls.Add(picBox);

            // =========================
            // OVERLAY
            // =========================

            overlay =
                new RaceOverlayPanel();

            overlay.Size =
                new Size(400, 600);

            EnableDoubleBuffer(overlay);

            picBox.Controls.Add(overlay);

            overlay.BringToFront();

            // =========================
            // HORSE PICTURE (TOP LEFT)
            // =========================

            PictureBox horsePic =
                new PictureBox();

            horsePic.Size =
                new Size(80, 80);

            horsePic.Location =
                new Point(18, 15);

            horsePic.BackColor =
                Color.Transparent;

            horsePic.SizeMode =
                PictureBoxSizeMode.Zoom;

            horsePic.Image = Properties.Resources.MejiroMcQueenProfile;

            EnableDoubleBuffer(horsePic);

            overlay.Controls.Add(horsePic);

            // =========================
            // HORSE NAME (RIGHT OF PICTURE)
            // =========================

            AddLabel(overlay, "Mejiro McQueen", 110, 45, 21);

            // =========================
            // STATS LAYER (drawn above the overlay fill)
            // =========================

            Panel statsLayer =
                new Panel();

            statsLayer.Size =
                overlay.ClientSize;

            statsLayer.Location =
                new Point(0, 0);

            statsLayer.BackColor =
                Color.Transparent;

            statsLayer.Paint +=
                StatsLayer_Paint;

            EnableDoubleBuffer(statsLayer);

            overlay.Controls.Add(statsLayer);

            // keep it behind the picture, name and back button
            statsLayer.SendToBack();

            // =========================
            // BACK BUTTON
            // =========================

            BackButton backButton =
                new BackButton();

            backButton.Location =
                new Point(
                    (overlay.Width -
                     backButton.Width) / 2,

                    overlay.Height -
                    backButton.Height -
                    10
                );

            backButton.Click +=
                BackButton_Click;

            overlay.Controls.Add(
                backButton
            );

            backButton.BringToFront();

            // =========================
            // PARALLAX TIMER
            // =========================

            UpdatePositions();

            parallaxTimer = new System.Windows.Forms.Timer();
            parallaxTimer.Interval = 16; // ~60 FPS
            parallaxTimer.Tick += ParallaxTimer_Tick;
            parallaxTimer.Start();
        }

        // =========================
        // PARALLAX UPDATE
        // =========================

        private void ParallaxTimer_Tick(object sender, EventArgs e)
        {
            // A tick can still be queued right as the form closes
            if (this.IsDisposed || !this.IsHandleCreated)
            {
                return;
            }

            // Cursor position relative to the form
            // (works even when hovering over the panel, picture, or buttons)
            Point mouse = this.PointToClient(Cursor.Position);

            float halfW = this.ClientSize.Width / 2f;
            float halfH = this.ClientSize.Height / 2f;

            float nx = Math.Max(-1f, Math.Min(1f, (mouse.X - halfW) / halfW));
            float ny = Math.Max(-1f, Math.Min(1f, (mouse.Y - halfH) / halfH));

            // Background moves opposite (far), panel moves with cursor (close)
            curBgX += (-nx * BgStrength - curBgX) * Smoothing;
            curBgY += (-ny * BgStrength - curBgY) * Smoothing;
            curPanelX += (nx * PanelStrength - curPanelX) * Smoothing;
            curPanelY += (ny * PanelStrength - curPanelY) * Smoothing;

            UpdatePositions();
        }

        private void UpdatePositions()
        {
            Point bgPos = new Point(
                (int)Math.Round(-BgMargin + curBgX),
                (int)Math.Round(-BgMargin + curBgY)
            );

            if (picBox.Location != bgPos)
            {
                picBox.Location = bgPos;
            }

            // Panel position on the FORM (centered + its own offset)
            int panelFormX = (this.ClientSize.Width - overlay.Width) / 2
                             + (int)Math.Round(curPanelX);

            int panelFormY = (this.ClientSize.Height - overlay.Height) / 2
                             + (int)Math.Round(curPanelY);

            // Convert to PictureBox coordinates (it is the parent)
            Point panelPos = new Point(
                panelFormX - picBox.Left,
                panelFormY - picBox.Top
            );

            // Only move when the pixel position actually changed (less flicker)
            if (overlay.Location != panelPos)
            {
                overlay.Location = panelPos;
            }
        }

        // =========================
        // REDUCE FLICKER
        // =========================

        // Paints the form and all child controls in one buffered pass
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED

                return cp;
            }
        }

        // DoubleBuffered is protected, so turn it on through reflection
        private static void EnableDoubleBuffer(Control control)
        {
            typeof(Control)
                .GetProperty(
                    "DoubleBuffered",
                    BindingFlags.Instance |
                    BindingFlags.NonPublic)
                .SetValue(control, true, null);
        }

        // =========================
        // DRAW BASE STATS + APTITUDE TABLES
        // =========================

        // Aptitude numbers: 1 = S, 2 = A, 3 = B, 4 = C, 5 = D, 6 = E, 7 = F, 8 = G

        private void StatsLayer_Paint(
            object sender,
            PaintEventArgs e)
        {
            Control panel = (Control)sender;
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            int w = panel.ClientSize.Width;

            float tx = 20f;          // table left
            float tw = w - 40f;      // table width

            using (Font titleFont = UmaText.Create(16, FontStyle.Bold))
            using (Font headFont = UmaText.Create(12, FontStyle.Bold))
            using (Font valueFont = UmaText.Create(13, FontStyle.Bold))
            using (Font starFont = UmaText.CreateSymbol(11, FontStyle.Regular))
            using (Pen border = new Pen(Color.FromArgb(120, 255, 255, 255)))
            using (SolidBrush headFill = new SolidBrush(Color.FromArgb(45, 255, 255, 255)))
            using (SolidBrush bodyFill = new SolidBrush(Color.FromArgb(18, 255, 255, 255)))
            {
                // =========================
                // BASE STATS TABLE
                // =========================

                DrawText(g, "Base stats", titleFont, 0, 108, w, 26);

                float bx = tx;
                float by = 138f;
                float rowH = 36f;
                float labelW = 70f;
                float statColW = (tw - labelW) / 5f;

                RectangleF baseRect = new RectangleF(bx, by, tw, rowH * 2);

                using (GraphicsPath path = RoundedRect(baseRect, 10))
                {
                    g.FillPath(bodyFill, path);

                    // shaded header row + label column
                    g.SetClip(path);
                    g.FillRectangle(headFill, bx, by, tw, rowH);
                    g.FillRectangle(headFill, bx, by, labelW, rowH * 2);
                    g.ResetClip();

                    g.DrawPath(border, path);
                }

                // inner lines
                g.DrawLine(border, bx, by + rowH, bx + tw, by + rowH);
                g.DrawLine(border, bx + labelW, by, bx + labelW, by + rowH * 2);

                string[] statNames = { "Speed", "Stamina", "Power", "Guts", "Wit" };
                string[] statValues = { "87", "136", "87", "125", "115" };

                // star label
                DrawStars(g, "★★★★★", starFont, bx, by + rowH, labelW, rowH);

                for (int i = 0; i < 5; i++)
                {
                    float cx = bx + labelW + i * statColW;

                    DrawText(g, statNames[i], headFont,
                        cx, by, statColW, rowH);

                    DrawText(g, statValues[i], valueFont,
                        cx, by + rowH, statColW, rowH);
                }

                // =========================
                // APTITUDE TABLE
                // =========================

                DrawText(g, "Aptitude", titleFont, 0, 235, w, 26);

                float ax = tx;
                float ay = 263f;
                float aRowH = 60f;
                float aLabelW = 90f;
                float aCellsW = tw - aLabelW;

                RectangleF aptRect = new RectangleF(ax, ay, tw, aRowH * 3);

                using (GraphicsPath path = RoundedRect(aptRect, 10))
                {
                    g.FillPath(bodyFill, path);

                    // shaded row-name column
                    g.SetClip(path);
                    g.FillRectangle(headFill, ax, ay, aLabelW, aRowH * 3);
                    g.ResetClip();

                    g.DrawPath(border, path);
                }

                // inner lines
                g.DrawLine(border, ax + aLabelW, ay, ax + aLabelW, ay + aRowH * 3);
                g.DrawLine(border, ax, ay + aRowH, ax + tw, ay + aRowH);
                g.DrawLine(border, ax, ay + aRowH * 2, ax + tw, ay + aRowH * 2);

                DrawAptRow(g, headFont, valueFont, border,
                    ax, ay, aLabelW, aCellsW, aRowH,
                    "Surface",
                    new[] { "Turf", "Dirt" },
                    new[] { "2", "6" });

                DrawAptRow(g, headFont, valueFont, border,
                    ax, ay + aRowH, aLabelW, aCellsW, aRowH,
                    "Distance",
                    new[] { "Sprint", "Mile", "Medium", "Long" },
                    new[] { "8", "7", "2", "2" });

                DrawAptRow(g, headFont, valueFont, border,
                    ax, ay + aRowH * 2, aLabelW, aCellsW, aRowH,
                    "Strategy",
                    new[] { "Front", "Pace", "Late", "End" },
                    new[] { "3", "2", "5", "7" });
            }
        }

        private void DrawAptRow(
            Graphics g,
            Font headFont,
            Font valueFont,
            Pen border,
            float x,
            float y,
            float labelW,
            float cellsW,
            float rowH,
            string rowName,
            string[] headers,
            string[] values)
        {
            // Row name, centered in the shaded column
            DrawText(g, rowName, headFont, x, y, labelW, rowH);

            float cellW = cellsW / headers.Length;

            for (int i = 0; i < headers.Length; i++)
            {
                float cx = x + labelW + i * cellW;

                // separator between cells in this row
                if (i > 0)
                {
                    g.DrawLine(border, cx, y, cx, y + rowH);
                }

                // name on top, number below
                DrawText(g, headers[i], headFont, cx, y + 6, cellW, 22);
                DrawText(g, values[i], valueFont, cx, y + 28, cellW, 26);
            }
        }

        private GraphicsPath RoundedRect(RectangleF r, float radius)
        {
            float d = radius * 2;

            GraphicsPath path = new GraphicsPath();

            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void DrawText(Graphics g, string text, Font font,
            float x, float y, float width, float height)
        {
            UmaText.DrawFit(g, text, font, new RectangleF(x, y, width, height));
        }

        private void DrawStars(Graphics g, string text, Font font,
            float x, float y, float width, float height)
        {
            float maxWidth = width - 6f;

            SizeF size = g.MeasureString(
                text, font, PointF.Empty, StringFormat.GenericTypographic);

            Font drawFont = font;
            bool owns = false;

            if (size.Width > maxWidth)
            {
                drawFont = new Font(
                    font.FontFamily,
                    Math.Max(6f, font.Size * (maxWidth / size.Width) * 0.98f),
                    font.Style,
                    font.Unit);
                owns = true;
            }

            using (StringFormat sf = new StringFormat(
                StringFormatFlags.NoWrap | StringFormatFlags.NoClip))
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                g.DrawString(text, drawFont, Brushes.Gold,
                    new RectangleF(x, y, width, height), sf);
            }

            if (owns)
                drawFont.Dispose();
        }

        // =========================
        // LABEL HELPER
        // =========================

        private void AddLabel(Control parent, string text, int x, int y, float fontPixelSize)
        {
            Label label = new Label();
            label.Text = text;
            label.ForeColor = Color.White;
            label.Font = UmaText.Create(fontPixelSize, FontStyle.Regular);
            label.AutoSize = true;
            label.UseCompatibleTextRendering = true;
            label.BackColor = Color.Transparent;
            label.Location = new Point(x, y);
            parent.Controls.Add(label);
        }

        // =========================
        // BACK BUTTON
        // =========================

        private void BackButton_Click(
            object sender,
            EventArgs e)
        {
            // Play back SFX
            sfx.Play();

            // Show PickARacePanel
            if (pick15 != null)
            {
                pick15.Show();
                pick15.BringToFront();
            }

            // Close this form
            this.Close();
        }

        // =========================
        // CLEANUP
        // =========================

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            parallaxTimer?.Stop();
            parallaxTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}