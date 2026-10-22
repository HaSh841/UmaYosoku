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
    public class FineMotion  : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public FineMotion()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Fine_Motion, new Size(52, 52));
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

            this.Click += FineMotion_Click;
        }

        private void FineMotion_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick21 = this.FindForm() as PickARacePanel;

            if (pick21 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            FineMotionData data21 = new FineMotionData(pick21);

            // hide race selcetion

            pick21.Hide();

            // show data form

            data21.Show();

            data21.BringToFront();
            data21.Activate();
        }

    }
}
