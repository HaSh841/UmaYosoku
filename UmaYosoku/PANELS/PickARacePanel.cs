using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.IO;

namespace UmaYosoku.PANELS
{
    public partial class PickARacePanel : Form
    {
        private SoundPlayer bgMusic;

        public PickARacePanel()
        {

            // form settings

            this.Text = "UmaYosoku";
            this.Size = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            // icon

            this.Icon = new Icon(new MemoryStream(Properties.Resources.oguri_icon));

            //bgm



            //bg

            PictureBox picBox = new PictureBox();
            picBox.Image = Properties.Resources.oguri2;
            picBox.Dock = DockStyle.Fill;
            picBox.SizeMode = PictureBoxSizeMode.Zoom;

            this.Controls.Add(picBox);

            //menu panel

            NewMenuPanel menu = new NewMenuPanel();
            menu.Size = new Size(1100, 620);
            menu.Location = new Point(
                (picBox.ClientSize.Width - menu.Width) / 2,
                (picBox.ClientSize.Height - menu.Height) / 2
                );

            picBox.Controls.Add(menu);
            menu.BringToFront();
        }
    }
}
