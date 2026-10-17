using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UmaYosoku.Button;
using UmaYosoku.PANELS;
using System.Media;
using System.Net.Http.Headers;

namespace UmaYosoku
{
    public class MenuPanel : Panel
    {
        private SoundPlayer sfx;

        // Controls kept as fields so they can float for the 3D effect
        private PickARace btn1;
        private PredictRace btn2;
        private Label title;

        // Resting positions (when the cursor is centered)
        private readonly Point btn1Base = new Point(40, 250);
        private readonly Point btn2Base = new Point(180, 250);
        private readonly Point titleBase = new Point(0, 170);

        // How much more than the panel each layer moves
        private const float ButtonDepth = 0.8f;
        private const float TitleDepth = 1.2f;

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

            btn1 =
                new PickARace();

            btn1.Location =
                btn1Base;

            sfx = new SoundPlayer(Properties.Resources.sfx1);

            btn1.Click +=
                PickArace_Click;


            // =========================
            // PREDICT RACE BUTTON
            // =========================

            btn2 =
                new PredictRace();

            btn2.Location =
                btn2Base;

            // title label

            title = new Label();
            title.Text = "Welcome to UmaYosoku!";
            title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.BackColor = Color.Transparent;
            title.AutoSize = false;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Size = new Size(this.Width, 60);
            title.Location = titleBase;

            // =========================
            // ADD BUTTONS
            // =========================

            this.Controls.Add(btn1);
            this.Controls.Add(btn2);
            this.Controls.Add(title);
        }

        // =========================
        // 3D DEPTH OFFSET
        // Called by Form1 every frame
        // =========================

        public void SetDepthOffset(float x, float y)
        {
            int bx = (int)System.Math.Round(x * ButtonDepth);
            int by = (int)System.Math.Round(y * ButtonDepth);

            int tx = (int)System.Math.Round(x * TitleDepth);
            int ty = (int)System.Math.Round(y * TitleDepth);

            btn1.Location = new Point(btn1Base.X + bx, btn1Base.Y + by);
            btn2.Location = new Point(btn2Base.X + bx, btn2Base.Y + by);
            title.Location = new Point(titleBase.X + tx, titleBase.Y + ty);
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



            // Create PickARacePanel
            PickARacePanel pick =
                new PickARacePanel(mainForm);

            // Hide Form1
            mainForm.Hide();

            // Show PickARacePanel
            pick.Show();

            sfx.Play();
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