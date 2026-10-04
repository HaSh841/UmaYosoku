using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using UmaYosoku.UMA_DATA;
using UmaYosoku.PANELS;
using UmaYosoku.Button;

namespace UmaYosoku.UMA_DATA
{
    public partial class AdmireVegaData : Form
    {
        private Label uma;
        private SoundPlayer sfx;
        private PickARacePanel pick;

        public AdmireVegaData(PickARacePanel pick)
        {
            // form settings


            this.pick = pick;
            this.Text = "UmaYosoku";
            this.Size = new Size(500, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Icon = new Icon(new MemoryStream(Properties.Resources.oguri_icon));

            // sfx

            sfx = new SoundPlayer(Properties.Resources.sfx1);

            PictureBox picBox = new PictureBox();
            picBox.Image = Properties.Resources.still_in_love;
            picBox.Dock = DockStyle.Fill;
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(picBox);

            // panel settings

            RaceOverlayPanel overlay = new RaceOverlayPanel();
            overlay.Size = new Size(400, 600);
            overlay.Location = new Point(
                (this.ClientSize.Width - overlay.Width) / 2,
                (this.ClientSize.Height - overlay.Height) / 2);

            picBox.Controls.Add(overlay);
            overlay.BringToFront();

            BackButton backButton = new BackButton();
            backButton.Location = new Point(
                (overlay.Width - backButton.Width) / 2,
                overlay.Height - backButton.Height - 15);

            backButton.Click += BackButton_Click;

            overlay.Controls.Add(backButton);
            backButton.BringToFront();
        }

        private void BackButton_Click(
           object sender,
           EventArgs e)
        {
            // Show original Form1
            if (pick != null)
            {
                pick.Show();
                pick.BringToFront();
            }

            // Close PickARacePanel
            this.Close();

            sfx.Play();
        }
    }
}

