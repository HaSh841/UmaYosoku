using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using UmaYosoku.PANELS;
using UmaYosoku.Button;

namespace UmaYosoku.UMA_DATA
{
    public partial class AdmireVegaData : Form
    {
        private SoundPlayer sfx;
        private PickARacePanel pick;

        public AdmireVegaData(PickARacePanel pick)
        {
            // =========================
            // STORE PREVIOUS FORM
            // =========================

            this.pick = pick;

            // =========================
            // FORM SETTINGS
            // =========================

            this.Text = "UmaYosoku";

            this.ClientSize =
                new Size(500, 700);

            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.MaximizeBox = false;

            this.MinimizeBox = true;

            this.FormBorderStyle =
                FormBorderStyle.FixedSingle;

            this.Icon = new Icon(
                new MemoryStream(
                    Properties.Resources.oguri_icon
                )
            );

            // =========================
            // BACK BUTTON SFX
            // =========================

            sfx = new SoundPlayer(
                Properties.Resources.sfx1
            );

            // =========================
            // BACKGROUND
            // =========================

            PictureBox picBox =
                new PictureBox();

            picBox.Image =
                Properties.Resources.admire_vega_bg;

            picBox.Dock =
                DockStyle.Fill;

            picBox.SizeMode =
                PictureBoxSizeMode.StretchImage;

            this.Controls.Add(picBox);

            // =========================
            // OVERLAY
            // =========================

            RaceOverlayPanel overlay =
                new RaceOverlayPanel();

            overlay.Size =
                new Size(400, 600);

            overlay.Location =
                new Point(
                    (this.ClientSize.Width -
                     overlay.Width) / 2,

                    (this.ClientSize.Height -
                     overlay.Height) / 2
                );

            picBox.Controls.Add(overlay);

            overlay.BringToFront();

            // =========================
            // HORSE NAME
            // =========================

            AddLabel(
                overlay,
                "Admire Vega",
                18,
                15,
                14
            );

            // =========================
            // BASE STATS
            // =========================

            AddLabel(
                overlay,
                "base stats",
                145,
                100,
                11
            );

            // =========================
            // STAT NAMES
            // =========================

            AddLabel(
                overlay,
                "speed",
                18,
                135,
                9
            );

            AddLabel(
                overlay,
                "stamina",
                82,
                135,
                9
            );

            AddLabel(
                overlay,
                "power",
                160,
                135,
                9
            );

            AddLabel(
                overlay,
                "guts",
                235,
                135,
                9
            );

            AddLabel(
                overlay,
                "wit",
                300,
                135,
                9
            );

            // =========================
            // STAT VALUES
            // =========================

            AddLabel(
                overlay,
                "118",
                27,
                165,
                9
            );

            AddLabel(
                overlay,
                "96",
                96,
                165,
                9
            );

            AddLabel(
                overlay,
                "132",
                168,
                165,
                9
            );

            AddLabel(
                overlay,
                "85",
                240,
                165,
                9
            );

            AddLabel(
                overlay,
                "119",
                300,
                165,
                9
            );

            // =========================
            // APTITUDE
            // =========================

            AddLabel(
                overlay,
                "Aptitude",
                158,
                225,
                11
            );

            // =========================
            // SURFACE
            // =========================

            AddLabel(
                overlay,
                "turf",
                95,
                270,
                9
            );

            AddLabel(
                overlay,
                "dirt",
                155,
                270,
                9
            );

            AddLabel(
                overlay,
                "Surface",
                10,
                300,
                9
            );

            AddLabel(
                overlay,
                "2",
                100,
                300,
                9
            );

            AddLabel(
                overlay,
                "8",
                160,
                300,
                9
            );

            // =========================
            // DISTANCE
            // =========================

            AddLabel(
                overlay,
                "sprint",
                85,
                335,
                9
            );

            AddLabel(
                overlay,
                "mile",
                145,
                335,
                9
            );

            AddLabel(
                overlay,
                "medium",
                195,
                335,
                9
            );

            AddLabel(
                overlay,
                "long",
                285,
                335,
                9
            );

            AddLabel(
                overlay,
                "Distance",
                10,
                365,
                9
            );

            AddLabel(
                overlay,
                "7",
                100,
                365,
                9
            );

            AddLabel(
                overlay,
                "4",
                155,
                365,
                9
            );

            AddLabel(
                overlay,
                "2",
                215,
                365,
                9
            );

            AddLabel(
                overlay,
                "4",
                300,
                365,
                9
            );

            // =========================
            // STRATEGY
            // =========================

            AddLabel(
                overlay,
                "front",
                85,
                400,
                9
            );

            AddLabel(
                overlay,
                "pace",
                145,
                400,
                9
            );

            AddLabel(
                overlay,
                "late",
                215,
                400,
                9
            );

            AddLabel(
                overlay,
                "end",
                300,
                400,
                9
            );

            AddLabel(
                overlay,
                "Strategy",
                10,
                430,
                9
            );

            AddLabel(
                overlay,
                "8",
                100,
                430,
                9
            );

            AddLabel(
                overlay,
                "8",
                155,
                430,
                9
            );

            AddLabel(
                overlay,
                "3",
                215,
                430,
                9
            );

            AddLabel(
                overlay,
                "2",
                300,
                430,
                9
            );

            // =========================
            // BACK BUTTON
            // =========================

            BackButton backButton =
                new BackButton();

            backButton.Location =
                new Point(
                    (overlay.Width -
                     backButton.Width) / 2,

                    overlay.Height -
                    backButton.Height -
                    10
                );

            backButton.Click +=
                BackButton_Click;

            overlay.Controls.Add(
                backButton
            );

            backButton.BringToFront();
        }

        // =========================
        // LABEL HELPER
        // =========================

        private void AddLabel(
            Control parent,
            string text,
            int x,
            int y,
            float fontSize)
        {
            Label label =
                new Label();

            label.Text = text;

            label.ForeColor =
                Color.White;

            label.Font =
                new Font(
                    "Segoe UI",
                    fontSize,
                    FontStyle.Regular
                );

            label.AutoSize = true;

            label.BackColor =
                Color.Transparent;

            label.Location =
                new Point(x, y);

            parent.Controls.Add(label);
        }

        // =========================
        // BACK BUTTON
        // =========================

        private void BackButton_Click(
            object sender,
            EventArgs e)
        {
            // Play back SFX
            sfx.Play();

            // Show PickARacePanel
            if (pick != null)
            {
                pick.Show();
                pick.BringToFront();
            }

            // Close this form
            this.Close();
        }
    }
}