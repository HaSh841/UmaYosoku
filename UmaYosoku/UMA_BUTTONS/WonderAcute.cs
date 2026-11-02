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
    public class WonderAcute : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public WonderAcute()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Wonder_Acute, new Size(52, 52));
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

            this.Click += WonderAcute_Click;
        }

        private void WonderAcute_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick81 = this.FindForm() as PickARacePanel;

            if (pick81 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            WonderAcuteData data81 = new WonderAcuteData(pick81);

            // hide race selcetion

            pick81.Hide();

            // show data form

            data81.Show();

            data81.BringToFront();
            data81.Activate();
        }

    }
}
