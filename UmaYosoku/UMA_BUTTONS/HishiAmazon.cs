using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
<<<<<<< HEAD
=======
using System.Windows.Forms.VisualStyles;
using System.Media;
using UmaYosoku.PANELS;
using UmaYosoku.UMA_DATA;
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9

namespace UmaYosoku.UMA_BUTTONS
{
    public class HishiAmazon : System.Windows.Forms.Button
    {
<<<<<<< HEAD
=======
        private SoundPlayer sfx;
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
        public HishiAmazon()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Hishi_Amazon_29, new Size(52, 52));
<<<<<<< HEAD

            this.ImageAlign = ContentAlignment.MiddleCenter;
=======
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9

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
<<<<<<< HEAD
        }
=======

            // sfx

            sfx = new SoundPlayer(Properties.Resources.weei);

            // click event

            this.Click += HishiAmazon_Click;
        }

        private void HishiAmazon_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick14 = this.FindForm() as PickARacePanel;

            if (pick14 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            HishiAmazonData data14 = new HishiAmazonData(pick14);

            // hide race selcetion

            pick14.Hide();

            // show data form

            data14.Show();

            data14.BringToFront();
            data14.Activate();
        }

>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
    }
}
