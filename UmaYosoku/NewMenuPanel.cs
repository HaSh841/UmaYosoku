using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UmaYosoku.Button;
using System.Windows;
using UmaYosoku.PANELS;

namespace UmaYosoku
{
    public class NewMenuPanel : Panel
    {
        public NewMenuPanel()
        {
            this.Size = new Size(320, 600);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
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