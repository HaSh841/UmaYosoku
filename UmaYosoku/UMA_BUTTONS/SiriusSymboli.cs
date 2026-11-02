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
    public class SiriusSymboli : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public SiriusSymboli()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Sirius_Symboli, new Size(52, 52));
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

            this.Click += SiriusSymboli_Click;
        }

        private void SiriusSymboli_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick67 = this.FindForm() as PickARacePanel;

            if (pick67 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            SiriusSymboliData data67 = new SiriusSymboliData(pick67);

            // hide race selcetion

            pick67.Hide();

            // show data form

            data67.Show();

            data67.BringToFront();
            data67.Activate();
        }

    }
}
