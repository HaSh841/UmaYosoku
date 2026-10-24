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
    public class HishiAkebono : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public HishiAkebono()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Hishi_Akebono, new Size(52, 52));
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

            this.Click += HishiAkebono_Click;
        }

        private void HishiAkebono_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick27 = this.FindForm() as PickARacePanel;

            if (pick27 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            HishiAkebonoData data27 = new HishiAkebonoData(pick27);

            // hide race selcetion

            pick27.Hide();

            // show data form

            data27.Show();

            data27.BringToFront();
            data27.Activate();
        }

    }
}
