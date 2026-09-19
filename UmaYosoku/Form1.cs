using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace UmaYosoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // =========================
            // FORM SETTINGS
            // =========================

            this.Text = "UmaYosoku";
            this.Size = new Size(400, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            // =========================
            // ICON
            // =========================

            this.Icon = new Icon(
                new MemoryStream(
                    Properties.Resources.oguri_icon
                )
            );

            // =========================
            // BACKGROUND
            // =========================

            PictureBox pictureBox = new PictureBox();

            pictureBox.Image =
                Properties.Resources.oguri_cap;

            pictureBox.Dock =
                DockStyle.Fill;

            pictureBox.SizeMode =
                PictureBoxSizeMode.StretchImage;

            this.Controls.Add(pictureBox);

            // =========================
            // MENU PANEL
            // =========================

            MenuPanel menuPanel =
                new MenuPanel();

            menuPanel.Size =
                new Size(320, 600);

            menuPanel.Location = new Point(
                (pictureBox.ClientSize.Width -
                 menuPanel.Width) / 2,

                (pictureBox.ClientSize.Height -
                 menuPanel.Height) / 2
            );

            pictureBox.Controls.Add(menuPanel);

            menuPanel.BringToFront();

            // =========================
            // DASHBOARD PANEL
            // =========================

            Dashboard dashboard =
                new Dashboard();

            dashboard.Size =
                new Size(250, 400);

            dashboard.Location = new Point(
                (pictureBox.ClientSize.Width -
                 dashboard.Width) / 2,

                (pictureBox.ClientSize.Height -
                 dashboard.Height) / 2
            );

            pictureBox.Controls.Add(dashboard);

            dashboard.BringToFront();
        }
    }
}