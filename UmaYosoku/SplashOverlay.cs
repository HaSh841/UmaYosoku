using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace UmaYosoku
{
    [DesignerCategory("")]
    [ToolboxItem(false)]
    public class SplashOverlay : Control
    {
        // =========================
        // LOOK SETTINGS
        // =========================

        private const float LogoScale = 0.35f;                 // logo size as a fraction of the window (was 0.5)
        private const int GapBetweenLogoAndText = 14;          // pixels
        private const string CreditText = "Powered by: J.M, J.B";

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image Logo { get; set; }

        // Picture of the finished UI, drawn under the white layer so the fade-out
        // is a flicker-free crossfade instead of a see-through control.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image Snapshot { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point SnapshotLocation { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float WhiteAlpha { get; set; } = 1f;  // 0..1

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float LogoAlpha { get; set; } = 0f;   // 0..1

        private readonly Font creditFont = new Font("Segoe UI", 10f, FontStyle.Regular);

        public SplashOverlay()
        {
            // Fully opaque + double buffered = no flicker
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.Opaque |
                ControlStyles.ResizeRedraw, true);

            DoubleBuffered = true;
            BackColor = Color.White;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Everything is drawn in OnPaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            float whiteA = Math.Max(0f, Math.Min(1f, WhiteAlpha));
            float logoA = Math.Max(0f, Math.Min(1f, LogoAlpha));

            // 1) Base layer: the UI snapshot if we have one, otherwise plain white
            if (Snapshot != null)
            {
                g.DrawImageUnscaled(Snapshot, SnapshotLocation);
            }
            else
            {
                g.Clear(Color.White);
            }

            // 2) White layer on top (fades out to reveal the UI)
            if (whiteA > 0f)
            {
                using (var brush = new SolidBrush(
                    Color.FromArgb((int)(whiteA * 255), 255, 255, 255)))
                {
                    g.FillRectangle(brush, ClientRectangle);
                }
            }

            // 3) Logo + credit text, centered as one block
            if (logoA <= 0f)
            {
                return;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            int logoW = 0, logoH = 0;
            if (Logo != null)
            {
                int size = (int)(Math.Min(Width, Height) * LogoScale);
                float ratio = (float)Logo.Width / Logo.Height;
                logoW = ratio >= 1 ? size : (int)(size * ratio);
                logoH = ratio >= 1 ? (int)(size / ratio) : size;
            }

            SizeF textSize = g.MeasureString(CreditText, creditFont);
            int blockH = logoH + GapBetweenLogoAndText + (int)Math.Ceiling(textSize.Height);
            int top = (Height - blockH) / 2;

            if (Logo != null)
            {
                var rect = new Rectangle((Width - logoW) / 2, top, logoW, logoH);
                var matrix = new ColorMatrix { Matrix33 = logoA };
                using (var attrs = new ImageAttributes())
                {
                    attrs.SetColorMatrix(matrix);
                    attrs.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(Logo, rect, 0, 0,
                        Logo.Width, Logo.Height, GraphicsUnit.Pixel, attrs);
                }
            }

            float textX = (Width - textSize.Width) / 2f;
            float textY = top + logoH + GapBetweenLogoAndText;
            using (var textBrush = new SolidBrush(
                Color.FromArgb((int)(logoA * 255), 90, 90, 90)))
            {
                g.DrawString(CreditText, creditFont, textBrush, textX, textY);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                creditFont.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}