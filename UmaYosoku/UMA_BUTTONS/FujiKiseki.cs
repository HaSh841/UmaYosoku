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
    public class FujiKiseki : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public FujiKiseki()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Fuji_Kiseki_29, new Size(52, 52));
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

            // sfx

            sfx = new SoundPlayer(Properties.Resources.weei);

            // click event

            this.Click += FujiKiseki_Click;
        }

        private void FujiKiseki_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick7 = this.FindForm() as PickARacePanel;

            if (pick7 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            FujiKisekiData data7 = new FujiKisekiData(pick7);

            // hide race selcetion

            pick7.Hide();

            // show data form

            data7.Show();

            data7.BringToFront();
            data7.Activate();
        }

    }
}
