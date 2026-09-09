using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UmaYosoku
{
    public class Dashboard : Panel
    {
        private Label titleLabel;
        private Label successRateLabel;
        private Label softVotingLabel;
        private Label accuracyLabel;
        private Label speedLabel;

        private int cornerRadius = 30;

        public Dashboard()
        {
            // =========================
            // PANEL SETTINGS
            // =========================

            this.Size = new Size(250, 400);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;

            SetRoundedRegion();


            // =========================
            // TITLE
            // =========================

            titleLabel = new Label();

            titleLabel.Text = "Results";
            titleLabel.ForeColor = Color.Black;
            titleLabel.Font = new Font(
                "Arial",
                12,
                FontStyle.Bold
            );

            titleLabel.AutoSize = false;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            titleLabel.Size = new Size(this.Width, 40);
            titleLabel.Location = new Point(0, 10);

            this.Controls.Add(titleLabel);


            // =========================
            // SUCCESS RATE
            // =========================

            successRateLabel = CreateLabel(
                "",
                70
            );

            this.Controls.Add(successRateLabel);


            // =========================
            // SOFT VOTING
            // =========================

            softVotingLabel = CreateLabel(
                "",
                110
            );

            this.Controls.Add(softVotingLabel);


            // =========================
            // ACCURACY
            // =========================

            accuracyLabel = CreateLabel(
                "",
                150
            );

            this.Controls.Add(accuracyLabel);


            // =========================
            // SPEED
            // =========================

            speedLabel = CreateLabel(
                "",
                190
            );

            this.Controls.Add(speedLabel);
        }


        // =========================
        // CREATE LABEL
        // =========================

        private Label CreateLabel(string text, int y)
        {
            Label label = new Label();

            label.Text = text;

            label.ForeColor = Color.Black;

            label.Font = new Font(
                "Arial",
                10,
                FontStyle.Bold
            );

            label.AutoSize = false;

            label.TextAlign = ContentAlignment.MiddleLeft;

            label.Size = new Size(270, 35);

            label.Location = new Point(15, y);

            label.BackColor = Color.Transparent;

            return label;
        }


        // =========================
        // ROUNDED REGION
        // =========================

        private void SetRoundedRegion()
        {
            if (this.Width <= 0 || this.Height <= 0)
                return;

            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = cornerRadius;

                path.AddArc(
                    0,
                    0,
                    radius,
                    radius,
                    180,
                    90
                );

                path.AddArc(
                    this.Width - radius - 1,
                    0,
                    radius,
                    radius,
                    270,
                    90
                );

                path.AddArc(
                    this.Width - radius - 1,
                    this.Height - radius - 1,
                    radius,
                    radius,
                    0,
                    90
                );

                path.AddArc(
                    0,
                    this.Height - radius - 1,
                    radius,
                    radius,
                    90,
                    90
                );

                path.CloseFigure();

                this.Region = new Region(path);
            }
        }


        // =========================
        // UPDATE ROUNDED REGION
        // =========================

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            SetRoundedRegion();
        }


        // =========================
        // PAINT PANEL
        // =========================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(
                0,
                0,
                this.Width - 1,
                this.Height - 1
            );

            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = cornerRadius;

                path.AddArc(
                    rect.X,
                    rect.Y,
                    radius,
                    radius,
                    180,
                    90
                );

                path.AddArc(
                    rect.Right - radius,
                    rect.Y,
                    radius,
                    radius,
                    270,
                    90
                );

                path.AddArc(
                    rect.Right - radius,
                    rect.Bottom - radius,
                    radius,
                    radius,
                    0,
                    90
                );

                path.AddArc(
                    rect.X,
                    rect.Bottom - radius,
                    radius,
                    radius,
                    90,
                    90
                );

                path.CloseFigure();


                // =========================
                // SEMI-TRANSPARENT WHITE
                // =========================

                using (SolidBrush brush =
                    new SolidBrush(
                        Color.FromArgb(
                            190,
                            255,
                            255,
                            255
                        )
                    ))
                {
                    e.Graphics.FillPath(
                        brush,
                        path
                    );
                }
            }
        }


        // =========================
        // UPDATE PREDICTION RESULTS
        // =========================

        public void UpdateResults(
            double successRate,
            double softVoting,
            double accuracy,
            double speed)
        {
            successRateLabel.Text =
                $"SUCCESS RATE: {successRate:0.##}%";

            softVotingLabel.Text =
                $"SOFT VOTING: {softVoting:0.##}%";

            accuracyLabel.Text =
                $"ACCURACY: {accuracy:0.##}%";

            speedLabel.Text =
                $"SPEED: {speed:0.##} SECONDS";
        }
    }
}