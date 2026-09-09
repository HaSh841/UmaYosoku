using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UmaYosoku.Button;

namespace UmaYosoku
{
    public class MenuPanel : Panel
    {
        public MenuPanel()
        {
            this.Size = new Size(300, 600);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;

            //pick a race button

            PickARace btn1 = new PickARace();
            btn1.Location = new Point(35, 40);

            //predict race button

            PredictRace btn2 = new PredictRace();
            btn2.Location = new Point(155, 40);

            this.Controls.Add(btn1);
            this.Controls.Add(btn2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(
                0,
                0,
                this.Width - 1,
                this.Height - 1
            );

            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 30;

                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(
                    rect.Right - radius,
                    rect.Y,
                    radius,
                    radius,
                    270,
                    90
                );
                path.AddArc(
                    rect.Right - radius,
                    rect.Bottom - radius,
                    radius,
                    radius,
                    0,
                    90
                );
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
                    new SolidBrush(Color.FromArgb(160, 0, 0, 0)))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }
    }
}