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
    public class SuperCreek : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public SuperCreek()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Super_Creek, new Size(52, 52));
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

            this.Click += SuperCreek_Click;
        }

        private void SuperCreek_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick42 = this.FindForm() as PickARacePanel;

            if (pick42 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            SuperCreekData data42 = new SuperCreekData(pick42);

            // hide race selcetion

            pick42.Hide();

            // show data form

            data42.Show();

            data42.BringToFront();
            data42.Activate();
        }

    }
}
