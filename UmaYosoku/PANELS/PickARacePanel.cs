using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using UmaYosoku.Button;

namespace UmaYosoku.PANELS
{
    public partial class PickARacePanel : Form
    {
        // Reference to the original Form1
        private Form1 mainForm;

        public PickARacePanel(Form1 mainForm)
        {
            this.mainForm = mainForm;

            // =========================
            // FORM SETTINGS
            // =========================

            this.Text = "UmaYosoku";

            this.Size =
                new Size(1280, 720);

            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.MaximizeBox = false;
            this.MinimizeBox = true;

            this.FormBorderStyle =
                FormBorderStyle.FixedSingle;

            // =========================
            // ICON
            // =========================

            this.Icon = new Icon(
                new MemoryStream(
                    Properties.Resources.oguri_icon
                )
            );

            // =========================
            // BACKGROUND IMAGE
            // =========================

            PictureBox picBox =
                new PictureBox();

            picBox.Image =
                Properties.Resources.oguri2;

            picBox.Dock =
                DockStyle.Fill;

            // Fit image to entire form
            picBox.SizeMode =
                PictureBoxSizeMode.StretchImage;

            this.Controls.Add(picBox);

            // =========================
            // SEMI-TRANSPARENT PANEL
            // =========================

            RaceOverlayPanel overlay =
                new RaceOverlayPanel();

            overlay.Size =
                new Size(1200, 620);

            overlay.Location = new Point(
                (this.ClientSize.Width -
                 overlay.Width) / 2,

                (this.ClientSize.Height -
                 overlay.Height) / 2
            );

            picBox.Controls.Add(overlay);

            overlay.BringToFront();

            // =========================
            // BACK BUTTON
            // =========================

            BackButton backBtn =
                new BackButton();

            backBtn.Location = new Point(
                (overlay.Width -
                 backBtn.Width) / 2,

                overlay.Height -
                backBtn.Height -
                15
            );

            backBtn.Click +=
                BackButton_Click;

            overlay.Controls.Add(backBtn);

            backBtn.BringToFront();
        }

        // =========================
        // BACK BUTTON
        // =========================

        private void BackButton_Click(
            object sender,
            EventArgs e)
        {
            // Show original Form1
            if (mainForm != null)
            {
                mainForm.Show();

                mainForm.BringToFront();

                // Play Form1 music again
                mainForm.PlayBackgroundMusic();
            }

            // Close PickARacePanel
            this.Close();
        }
    }


    // ==========================================
    // SEMI-TRANSPARENT BLACK PANEL
    // ==========================================

    public class RaceOverlayPanel : Panel
    {
        public RaceOverlayPanel()
        {
            this.BackColor =
                Color.Transparent;

            this.DoubleBuffered = true;
        }

        protected override void OnPaint(
            PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle rect =
                new Rectangle(
                    0,
                    0,
                    this.Width - 1,
                    this.Height - 1
                );

            using (GraphicsPath path =
                new GraphicsPath())
            {
                int radius = 30;

                // TOP LEFT
                path.AddArc(
                    rect.X,
                    rect.Y,
                    radius,
                    radius,
                    180,
                    90
                );

                // TOP RIGHT
                path.AddArc(
                    rect.Right - radius,
                    rect.Y,
                    radius,
                    radius,
                    270,
                    90
                );

                // BOTTOM RIGHT
                path.AddArc(
                    rect.Right - radius,
                    rect.Bottom - radius,
                    radius,
                    radius,
                    0,
                    90
                );

                // BOTTOM LEFT
                path.AddArc(
                    rect.X,
                    rect.Bottom - radius,
                    radius,
                    radius,
                    90,
                    90
                );

                path.CloseFigure();

                // Semi-transparent black
                using (SolidBrush brush =
                    new SolidBrush(
                        Color.FromArgb(
                            160,
                            0,
                            0,
                            0
                        )))
                {
                    e.Graphics.FillPath(
                        brush,
                        path
                    );
                }
            }
        }
    }
}