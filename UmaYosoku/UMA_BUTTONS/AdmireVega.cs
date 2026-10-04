using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using UmaYosoku.PANELS;
using UmaYosoku.UMA_DATA;

namespace UmaYosoku.UMA_BUTTONS
{
    public class AdmireVega : System.Windows.Forms.Button
    {
        private SoundPlayer sfx;

        public AdmireVega()
        {
            // =========================
            // BUTTON SIZE
            // =========================

            this.Size = new Size(65, 65);

            // =========================
            // IMAGE
            // =========================

            this.Image = new Bitmap(
                Properties.Resources.Admire_Vega_29,
                new Size(52, 52)
            );

            this.ImageAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // =========================
            // BUTTON STYLE
            // =========================

            this.FlatStyle = FlatStyle.Flat;

            this.FlatAppearance.BorderSize = 0;

            this.FlatAppearance.BorderColor =
                Color.White;

            this.BackColor =
                Color.Transparent;

            this.FlatAppearance.MouseOverBackColor =
                Color.Gray;

            this.FlatAppearance.MouseDownBackColor =
                Color.DimGray;

            // =========================
            // NO TEXT
            // =========================

            this.Text = "";

            // =========================
            // CURSOR
            // =========================

            this.Cursor =
                Cursors.Hand;

            // =========================
            // SFX
            // =========================

            sfx = new SoundPlayer(
                Properties.Resources.weei
            );

            // =========================
            // CLICK EVENT
            // =========================

            this.Click +=
                AdmireVega_Click;
        }

        private void AdmireVega_Click(
            object sender,
            EventArgs e)
        {
            // Find PickARacePanel
            PickARacePanel pick =
                this.FindForm() as PickARacePanel;

            if (pick == null)
            {
                MessageBox.Show(
                    "PickARacePanel was not found."
                );

                return;
            }

            // Play SFX
            sfx.Play();

            // Create data form
            AdmireVegaData data =
                new AdmireVegaData(pick);

            // Hide race selection
            pick.Hide();

            // Show data form
            data.Show();

            data.BringToFront();
            data.Activate();
        }
    }
}