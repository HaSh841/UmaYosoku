using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Media;

namespace UmaYosoku
{
    public partial class Form1 : Form

    {
        private SoundPlayer backgroundMusic;

        public Form1()
        {
            InitializeComponent();

            // Form settings
            this.Text = "UmaYosoku";
            this.Size = new Size(400, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.MaximizeBox = false;

            // Icon
            this.Icon = new Icon(
                new MemoryStream(Properties.Resources.oguri_icon)



            );
            
            //============
            //bgm
            //============

            backgroundMusic = new SoundPlayer(Properties.Resources.BRIGHTEST_HEART);
            backgroundMusic.PlayLooping();

            // =========================
            // BACKGROUND GIF
            // =========================

            PictureBox pictureBox = new PictureBox();

            pictureBox.Image = Properties.Resources.oguri_cap;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(pictureBox);


            // =========================
            // MENU PANEL
            // =========================

            MenuPanel menuPanel = new MenuPanel();

            menuPanel.Size = new Size(300, 600);

            menuPanel.Location = new Point(
                (pictureBox.ClientSize.Width - menuPanel.Width) / 2,
                (pictureBox.ClientSize.Height - menuPanel.Height) / 2
            );

            // IMPORTANT:
            // Put MenuPanel INSIDE the PictureBox
            pictureBox.Controls.Add(menuPanel);

            menuPanel.BringToFront();

            //==============
            // DASHBOARD PANEL
            //==============

            Dashboard dashboard = new Dashboard();

            dashboard.Size = new Size(250, 400);

            dashboard.Location = new Point(
                (pictureBox.ClientSize.Width - dashboard.Width) / 2,
                (pictureBox.ClientSize.Height - dashboard.Height) / 2
            );

            pictureBox.Controls.Add(dashboard);

            dashboard.BringToFront();


        }
    }
}

