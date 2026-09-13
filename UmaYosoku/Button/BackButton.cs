using System.Drawing;
using System.Windows.Forms;

namespace UmaYosoku.Button
{
    public class BackButton : System.Windows.Forms.Button
    {
        public BackButton()
        {
            // =========================
            // TEXT
            // =========================

            this.Text = "<-";

            // =========================
            // SIZE
            // =========================

            this.Size =
                new Size(50, 40);

            // =========================
            // APPEARANCE
            // =========================

            this.ForeColor =
                Color.White;

            this.BackColor =
                Color.Transparent;

            this.FlatStyle =
                FlatStyle.Flat;

            // Remove border
            this.FlatAppearance.BorderSize = 0;

            // Remove hover background
            this.FlatAppearance.MouseOverBackColor =
                Color.Transparent;

            // Remove click background
            this.FlatAppearance.MouseDownBackColor =
                Color.Transparent;

            // =========================
            // FONT
            // =========================

            this.Font =
                new Font(
                    "Segoe UI",
                    14F,
                    FontStyle.Bold
                );

            // =========================
            // OTHER
            // =========================

            this.Cursor =
                Cursors.Hand;

            this.TabStop = false;

            this.UseVisualStyleBackColor =
                false;
        }
    }
}