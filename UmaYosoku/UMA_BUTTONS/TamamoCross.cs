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
    public class TamamoCross : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public TamamoCross()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Tamamo_Cross, new Size(52, 52));
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

            this.Click += TamamoCross_Click;
        }

        private void TamamoCross_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick20 = this.FindForm() as PickARacePanel;

            if (pick20 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            TamamoCrossData data20 = new TamamoCrossData(pick20);

            // hide race selcetion

            pick20.Hide();

            // show data form

            data20.Show();

            data20.BringToFront();
            data20.Activate();
        }

    }
}
