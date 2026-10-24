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
    public class HishiAkebono : System.Windows.Forms.Button
    {
<<<<<<< HEAD
=======
        private SoundPlayer sfx;
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
        public HishiAkebono()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Hishi_Akebono, new Size(52, 52));
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

>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
    }
}
