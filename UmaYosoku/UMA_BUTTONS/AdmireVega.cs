using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Media;
using UmaYosoku.PANELS;
using UmaYosoku.UMA_DATA;

namespace UmaYosoku.UMA_BUTTONS
{
    public class AdmireVega : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public AdmireVega()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Admire_Vega_29, new Size(52, 52));
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // button style

            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = Color.White;
            this.BackColor = Color.Transparent;
            this.FlatAppearance.MouseOverBackColor = Color.Gray;
            this.FlatAppearance.MouseDownBackColor = Color.DimGray;

            // no text

            this.Text = "";

            // cursor

            this.Cursor = Cursors.Hand;

            // action listener

            this.Click += AdmireVega_Click;

            sfx = new SoundPlayer(Properties.Resources.weei);

        }

        private void AdmireVega_Click(object sender, EventArgs e)
        {
            PickARacePanel pick = this.FindForm() as PickARacePanel;

            if (pick == null) return;

            AdmireVegaData data = new AdmireVegaData(pick);

            pick.Hide();
            data.Show();
            sfx.Play();
        }
    }
}
