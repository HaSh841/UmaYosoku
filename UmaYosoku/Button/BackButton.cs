using System.Drawing;
using System.Windows.Forms;

namespace UmaYosoku.Button
{
    public class BackButton : System.Windows.Forms.Button
    {
        public BackButton()
        {
            Text = "<-";

            Size = new Size(50, 40);

            ForeColor = Color.White;
            BackColor = Color.Transparent;

            FlatStyle = FlatStyle.Flat;

            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;

            Font = new Font(
                "Segoe UI",
                14F,
                FontStyle.Bold
            );

            Cursor = Cursors.Hand;
            TabStop = false;

            UseVisualStyleBackColor = false;
        }
    }
}