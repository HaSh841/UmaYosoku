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
    public class BiwaHayahide : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public BiwaHayahide()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Biwa_Hayahide, new Size(52, 52));
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

            this.Click += BiwaHayahide_Click;
        }

        private void BiwaHayahide_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick22 = this.FindForm() as PickARacePanel;

            if (pick22 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            BiwaHayahideData data22 = new BiwaHayahideData(pick22);

            // hide race selcetion

            pick22.Hide();

            // show data form

            data22.Show();

            data22.BringToFront();
            data22.Activate();
        }

    }
}
