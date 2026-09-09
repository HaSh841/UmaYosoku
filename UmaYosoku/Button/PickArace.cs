using System.Drawing;
using System.Windows.Forms;

namespace UmaYosoku.Button
{
    public class PickARace : System.Windows.Forms.Button
    {
        public PickARace()
        {
            this.Text = "PICK A\nRACE";

            this.Size = new Size(100, 40);

            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.White;
            this.FlatAppearance.BorderSize = 1;

            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;

            FlatAppearance.MouseOverBackColor = Color.Gray;
            FlatAppearance.MouseDownBackColor = Color.DarkGray;


            this.Font = new Font(
                "Arial",
                7,
                FontStyle.Bold
            );

            this.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}