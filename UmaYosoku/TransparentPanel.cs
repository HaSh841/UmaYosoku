using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace UmaYosoku
{
    public class TransparentPanel : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; } = 30;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Opacity { get; set; } = 160;

        public TransparentPanel()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
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
                int r = CornerRadius;

                path.AddArc(rect.X, rect.Y, r, r, 180, 90);
                path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
                path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
                path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);

                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(
                    Color.FromArgb(Opacity, Color.Black)))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }
    }
}