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

        private const float LogoScale = 0.35f;                 // logo size as a fraction of the window
        private const int GapBetweenLogoAndText = 14;          // pixels
        private const string CreditText = "Powered by: J.M, J.B";

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image Logo { get; set; }

        // Picture of the finished UI, drawn under the splash layer so the fade-out
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

        // White background + logo + credit text baked into ONE image.
        // While fading out, this single layer is faded as a whole, so the logo
        // and the white background disappear at exactly the same rate.
        private Bitmap layerCache;

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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            layerCache?.Dispose();
            layerCache = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            float whiteA = Math.Max(0f, Math.Min(1f, WhiteAlpha));
            float logoA = Math.Max(0f, Math.Min(1f, LogoAlpha));

            if (whiteA >= 1f)
            {
                // INTRO PHASE: solid white, logo fading in on top
                g.Clear(Color.White);
                if (logoA > 0f)
                {
                    DrawLogoBlock(g, logoA);
                }
                return;
            }

            // FADE-OUT PHASE: UI snapshot underneath, white + logo faded as ONE layer
            if (Snapshot != null)
            {
                g.DrawImageUnscaled(Snapshot, SnapshotLocation);
            }
            else
            {
                g.Clear(Color.White);
            }

            if (whiteA <= 0f || Width <= 0 || Height <= 0)
            {
                return;
            }

            EnsureLayer();

            var matrix = new ColorMatrix { Matrix33 = whiteA };
            using (var attrs = new ImageAttributes())
            {
                attrs.SetColorMatrix(matrix);
                attrs.SetWrapMode(WrapMode.TileFlipXY);
                g.DrawImage(
                    layerCache,
                    new Rectangle(0, 0, Width, Height),
                    0, 0, layerCache.Width, layerCache.Height,
                    GraphicsUnit.Pixel,
                    attrs);
            }
        }

        // Builds the white + logo + text image once (rebuilt only if the size changes)
        private void EnsureLayer()
        {
            if (layerCache != null
                && layerCache.Width == Width
                && layerCache.Height == Height)
            {
                return;
            }

            layerCache?.Dispose();
            layerCache = new Bitmap(Width, Height, PixelFormat.Format32bppPArgb);

            using (Graphics lg = Graphics.FromImage(layerCache))
            {
                lg.Clear(Color.White);
                DrawLogoBlock(lg, 1f);
            }
        }

        // Logo + credit text, centered as one block, drawn with the given alpha
        private void DrawLogoBlock(Graphics g, float alpha)
        {
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
                var matrix = new ColorMatrix { Matrix33 = alpha };
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
                Color.FromArgb((int)(alpha * 255), 90, 90, 90)))
            {
                g.DrawString(CreditText, creditFont, textBrush, textX, textY);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                creditFont.Dispose();
                layerCache?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}