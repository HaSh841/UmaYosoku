using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
<<<<<<< HEAD
=======
using System.Media;
using UmaYosoku.PANELS;
using UmaYosoku.UMA_DATA;
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9

namespace UmaYosoku.UMA_BUTTONS
{
    public class FujiKiseki : System.Windows.Forms.Button
    {
<<<<<<< HEAD
=======
        private SoundPlayer sfx;
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
        public FujiKiseki()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Fuji_Kiseki_29, new Size(52, 52));
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;

<<<<<<< HEAD
            // btn style
=======
            // button style
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9

            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = Color.White;
            this.BackColor = Color.Transparent;
            this.FlatAppearance.MouseOverBackColor = Color.Gray;
            this.FlatAppearance.MouseDownBackColor = Color.DimGray;

<<<<<<< HEAD
            this.Text = "";
            this.Cursor = Cursors.Hand;
        }
=======
            // no text

            this.Text = "";

            // cursor

            this.Cursor = Cursors.Hand;

            // sfx

            sfx = new SoundPlayer(Properties.Resources.weei);

            // click event

            this.Click += FujiKiseki_Click;
        }

        private void FujiKiseki_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick7 = this.FindForm() as PickARacePanel;

            if (pick7 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            FujiKisekiData data7 = new FujiKisekiData(pick7);

            // hide race selcetion

            pick7.Hide();

            // show data form

            data7.Show();

            data7.BringToFront();
            data7.Activate();
        }

>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
    }
}
