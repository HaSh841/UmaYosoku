using System;
using System.Drawing;
using System.Drawing.Text;

namespace UmaYosoku.UMA_DATA
{
    // Shared text helper for every Uma data form.
    //  - Fonts are sized in PIXELS, so Windows DPI scaling can't make them bigger than the cells.
    //  - If a font is missing on a PC, it falls back to one that exists.
    //  - Text that is still too wide for its cell is shrunk to fit instead of being cut off.
    internal static class UmaText
    {
        private static string textFamily;
        private static string symbolFamily;

        public static string TextFamily
        {
            get
            {
                if (textFamily == null)
                    textFamily = FirstInstalled(
                        "Segoe UI", "Arial", "Tahoma", "Microsoft Sans Serif");
                return textFamily;
            }
        }

        public static string SymbolFamily
        {
            get
            {
                if (symbolFamily == null)
                    symbolFamily = FirstInstalled(
                        "Segoe UI Symbol", "Arial Unicode MS", "Segoe UI", "Arial");
                return symbolFamily;
            }
        }

        private static string FirstInstalled(params string[] candidates)
        {
            try
            {
                using (InstalledFontCollection installed = new InstalledFontCollection())
                {
                    foreach (string name in candidates)
                    {
                        foreach (FontFamily f in installed.Families)
                        {
                            if (string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase))
                                return f.Name;
                        }
                    }
                }
            }
            catch
            {
                // ignore and use the system default below
            }

            return SystemFonts.DefaultFont.FontFamily.Name;
        }

        // Size is in pixels (12pt ~ 16px, 10pt ~ 13px, 9pt ~ 12px, 8pt ~ 11px)
        public static Font Create(float pixelSize, FontStyle style)
        {
            return new Font(TextFamily, pixelSize, style, GraphicsUnit.Pixel);
        }

        public static Font CreateSymbol(float pixelSize, FontStyle style)
        {
            return new Font(SymbolFamily, pixelSize, style, GraphicsUnit.Pixel);
        }

        // Draws centered text (with a soft shadow) that always fits inside rect
        public static void DrawFit(
            Graphics g,
            string text,
            Font font,
            RectangleF rect)
        {
            if (string.IsNullOrEmpty(text))
                return;

            text = text.Trim();

            float maxWidth = rect.Width - 4f;
            Font drawFont = font;
            bool ownsFont = false;

            try
            {
                SizeF size = g.MeasureString(
                    text, font, PointF.Empty, StringFormat.GenericTypographic);

                if (size.Width > maxWidth && maxWidth > 0)
                {
                    float newSize = Math.Max(
                        6f, font.Size * (maxWidth / size.Width) * 0.98f);

                    drawFont = new Font(
                        font.FontFamily, newSize, font.Style, font.Unit);
                    ownsFont = true;
                }

                using (StringFormat sf = new StringFormat(
                    StringFormatFlags.NoWrap | StringFormatFlags.NoClip))
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.None;

                    // soft shadow
                    g.DrawString(
                        text, drawFont, Brushes.Black,
                        new RectangleF(rect.X + 1, rect.Y + 1, rect.Width, rect.Height), sf);

                    // main text
                    g.DrawString(text, drawFont, Brushes.White, rect, sf);
                }
            }
            finally
            {
                if (ownsFont)
                    drawFont.Dispose();
            }
        }
    }
}