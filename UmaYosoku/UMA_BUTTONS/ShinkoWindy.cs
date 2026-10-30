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
    public class ShinkoWindy : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public ShinkoWindy()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Shinko_Windy, new Size(52, 52));
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

            this.Click += ShinkoWindy_Click;
        }

        private void ShinkoWindy_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick40 = this.FindForm() as PickARacePanel;

            if (pick40 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            ShinkoWindyData data40 = new ShinkoWindyData(pick40);

            // hide race selcetion

            pick40.Hide();

            // show data form

            data40.Show();

            data40.BringToFront();
            data40.Activate();
        }

    }
}
