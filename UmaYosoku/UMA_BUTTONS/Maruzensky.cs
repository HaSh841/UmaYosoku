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
    public class Maruzensky : System.Windows.Forms.Button
    {
<<<<<<< HEAD
=======
        private SoundPlayer sfx;
>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
        public Maruzensky()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Maruzensky_29, new Size(52, 52));
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

            this.Click += Maruzensky_Click;
        }

        private void Maruzensky_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick6 = this.FindForm() as PickARacePanel;

            if (pick6 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            MaruzenskyData data6 = new MaruzenskyData(pick6);

            // hide race selcetion

            pick6.Hide();

            // show data form

            data6.Show();

            data6.BringToFront();
            data6.Activate();
        }

>>>>>>> cff501a21142b80cd2245c27c4ac43acc2a37ec9
    }
}
