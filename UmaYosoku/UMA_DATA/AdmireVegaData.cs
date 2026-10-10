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
    public partial class AdmireVegaData : Form
    {
        private SoundPlayer sfx;
        private PickARacePanel pick;

        public AdmireVegaData(PickARacePanel pick)
        {
            // =========================
            // STORE PREVIOUS FORM
            // =========================

            this.pick = pick;

            // =========================
            // FORM SETTINGS
            // =========================

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
            // =========================

            PictureBox picBox =
                new PictureBox();

            picBox.Image =
                Properties.Resources.admire_vega_bg;

            picBox.Dock =
                DockStyle.Fill;

            picBox.SizeMode =
                PictureBoxSizeMode.StretchImage;

            EnableDoubleBuffer(picBox);

            this.Controls.Add(picBox);

            // =========================
            // OVERLAY
            // =========================

            RaceOverlayPanel overlay =
                new RaceOverlayPanel();

            overlay.Size =
                new Size(400, 600);

            overlay.Location =
                new Point(
                    (this.ClientSize.Width -
                     overlay.Width) / 2,

                    (this.ClientSize.Height -
                     overlay.Height) / 2
                );

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

            horsePic.Image = Properties.Resources.admire_admire;

            EnableDoubleBuffer(horsePic);

            overlay.Controls.Add(horsePic);

            // =========================
            // HORSE NAME (RIGHT OF PICTURE)
            // =========================

            AddLabel(overlay, "Admire Vega", 110, 60, 16);

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
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            int w = panel.ClientSize.Width;

            float tx = 20f;          // table left
            float tw = w - 40f;      // table width

            using (Font titleFont = new Font("Segoe UI", 12, FontStyle.Bold))
            using (Font headFont = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font valueFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (Font starFont = new Font("Segoe UI Symbol", 8))
            using (Pen border = new Pen(Color.FromArgb(120, 255, 255, 255)))
            using (SolidBrush headFill = new SolidBrush(Color.FromArgb(45, 255, 255, 255)))
            using (SolidBrush bodyFill = new SolidBrush(Color.FromArgb(18, 255, 255, 255)))
            using (StringFormat center = new StringFormat())
            {
                center.Alignment = StringAlignment.Center;
                center.LineAlignment = StringAlignment.Center;
                center.FormatFlags =
                    StringFormatFlags.NoWrap | StringFormatFlags.NoClip;

                // =========================
                // BASE STATS TABLE
                // =========================

                DrawText(g, "Base stats", titleFont, center, 0, 108, w, 26);

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
                string[] statValues = { "118", "96", "132", "85", "119" };

                // star label
                g.DrawString(
                    "★★★★★",
                    starFont,
                    Brushes.Gold,
                    new RectangleF(bx, by + rowH, labelW, rowH),
                    center
                );

                for (int i = 0; i < 5; i++)
                {
                    float cx = bx + labelW + i * statColW;

                    DrawText(g, statNames[i], headFont, center,
                        cx, by, statColW, rowH);

                    DrawText(g, statValues[i], valueFont, center,
                        cx, by + rowH, statColW, rowH);
                }

                // =========================
                // APTITUDE TABLE
                // =========================

                DrawText(g, "Aptitude", titleFont, center, 0, 235, w, 26);

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

                DrawAptRow(g, headFont, valueFont, center, border,
                    ax, ay, aLabelW, aCellsW, aRowH,
                    "Surface",
                    new[] { "Turf", "Dirt" },
                    new[] { "2", "8" });

                DrawAptRow(g, headFont, valueFont, center, border,
                    ax, ay + aRowH, aLabelW, aCellsW, aRowH,
                    "Distance",
                    new[] { "Sprint", "Mile", "Medium", "Long" },
                    new[] { "7", "4", "2", "4" });

                DrawAptRow(g, headFont, valueFont, center, border,
                    ax, ay + aRowH * 2, aLabelW, aCellsW, aRowH,
                    "Strategy",
                    new[] { "Front", "Pace", "Late", "End" },
                    new[] { "8", "8", "3", "2" });
            }
        }

        private void DrawAptRow(
            Graphics g,
            Font headFont,
            Font valueFont,
            StringFormat center,
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
            DrawText(g, rowName, headFont, center, x, y, labelW, rowH);

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
                DrawText(g, headers[i], headFont, center, cx, y + 6, cellW, 22);
                DrawText(g, values[i], valueFont, center, cx, y + 28, cellW, 26);
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

        private void DrawText(
            Graphics g,
            string text,
            Font font,
            StringFormat format,
            float x,
            float y,
            float width,
            float height)
        {
            // soft shadow
            g.DrawString(
                text,
                font,
                Brushes.Black,
                new RectangleF(x + 1, y + 1, width, height),
                format
            );

            // main text
            g.DrawString(
                text,
                font,
                Brushes.White,
                new RectangleF(x, y, width, height),
                format
            );
        }

        // =========================
        // LABEL HELPER
        // =========================

        private void AddLabel(
            Control parent,
            string text,
            int x,
            int y,
            float fontSize)
        {
            Label label =
                new Label();

            label.Text = text;

            label.ForeColor =
                Color.White;

            label.Font =
                new Font(
                    "Segoe UI",
                    fontSize,
                    FontStyle.Regular
                );

            label.AutoSize = true;

            label.BackColor =
                Color.Transparent;

            label.Location =
                new Point(x, y);

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
            if (pick != null)
            {
                pick.Show();
                pick.BringToFront();
            }

            // Close this form
            this.Close();
        }
    }
}