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
    public class AstonMachan : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;
        public AstonMachan()
        {
            // button size

            this.Size = new Size(65, 65);

            // image

            this.Image = new Bitmap(Properties.Resources.Aston_Machan, new Size(52, 52));
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

            this.Click += AstonMachan_Click;
        }

        private void AstonMachan_Click(object sender, EventArgs e)
        {
            // find pickarace panel

            PickARacePanel pick78 = this.FindForm() as PickARacePanel;

            if (pick78 == null)
            {
                MessageBox.Show("PickARacePanel was not found.");
                return;
            }
            sfx.Play();

            // create data form

            AstonMachanData data78 = new AstonMachanData(pick78);

            // hide race selcetion

            pick78.Hide();

            // show data form

            data78.Show();

            data78.BringToFront();
            data78.Activate();
        }

    }
}
