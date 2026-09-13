using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UmaYosoku.Button;
using UmaYosoku.PANELS;

namespace UmaYosoku
{
    public class MenuPanel : Panel
    {
        public MenuPanel()
        {
            // =========================
            // PANEL SETTINGS
            // =========================

            this.Size =
                new Size(320, 600);

            this.BackColor =
                Color.Transparent;

            this.DoubleBuffered = true;

            // =========================
            // PICK A RACE BUTTON
            // =========================

            PickARace btn1 =
                new PickARace();

            btn1.Location =
                new Point(40, 40);

            btn1.Click +=
                PickArace_Click;

            // =========================
            // PREDICT RACE BUTTON
            // =========================

            PredictRace btn2 =
                new PredictRace();

            btn2.Location =
                new Point(180, 40);

            // =========================
            // ADD BUTTONS
            // =========================

            this.Controls.Add(btn1);
            this.Controls.Add(btn2);
        }

        // =========================
        // PICK A RACE
        // =========================

        private void PickArace_Click(
            object sender,
            System.EventArgs e)
        {
            // Get the existing Form1
            Form1 mainForm =
                this.FindForm() as Form1;

            if (mainForm == null)
            {
                return;
            }

            // Stop Form1 music
            mainForm.StopBackgroundMusic();

            // Create PickARacePanel
            PickARacePanel pick =
                new PickARacePanel(mainForm);

            // Hide Form1
            mainForm.Hide();

            // Show PickARacePanel
            pick.Show();
        }

        // =========================
        // DRAW MENU PANEL
        // =========================

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