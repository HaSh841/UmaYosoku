using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using UmaYosoku.Button;
using System.Media;
using UmaYosoku.UMA_BUTTONS;

namespace UmaYosoku.PANELS
{
    public partial class PickARacePanel : Form
    {
        // Reference to the original Form1
        private Form1 mainForm;

        private SoundPlayer sfx;
        public PickARacePanel(Form1 mainForm)
        {
            this.mainForm = mainForm;

            // =========================
            // FORM SETTINGS
            // =========================

            this.Text = "UmaYosoku";

            this.Size =
                new Size(1280, 720);

            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.MaximizeBox = false;
            this.MinimizeBox = true;

            this.FormBorderStyle =
                FormBorderStyle.FixedSingle;

            // =========================
            // ICON
            // =========================

            this.Icon = new Icon(
                new MemoryStream(
                    Properties.Resources.oguri_icon
                )
            );

            // =========================
            // BACKGROUND IMAGE
            // =========================

            PictureBox picBox =
                new PictureBox();

            picBox.Image =
                Properties.Resources.oguri2;

            picBox.Dock =
                DockStyle.Fill;

            picBox.SizeMode =
                PictureBoxSizeMode.StretchImage;

            this.Controls.Add(picBox);

            
            // =========================
            // SEMI-TRANSPARENT PANEL
            // =========================

            RaceOverlayPanel overlay =
                new RaceOverlayPanel();

            overlay.Size =
                new Size(1200, 620);

            overlay.Location = new Point(
                (this.ClientSize.Width -
                 overlay.Width) / 2,

                (this.ClientSize.Height -
                 overlay.Height) / 2
            );

            picBox.Controls.Add(overlay);

            overlay.BringToFront();

            // admire groove

            AdmireGroove admire = new AdmireGroove();
            admire.Location = new Point(40, 30);
            overlay.Controls.Add(admire);

            // admire vega

            AdmireVega vega = new AdmireVega();
            vega.Location = new Point(admire.Right + 10, 30);
            overlay.Controls.Add(vega);

            vega.BringToFront();

            // agnes digital

            AgnesDigital digital = new AgnesDigital();
            digital.Location = new Point(vega.Right + 10, 30);
            overlay.Controls.Add(digital);

            digital.BringToFront();

            // special week

            SpecialWeek week = new SpecialWeek();
            week.Location = new Point(digital.Right + 10, 30);
            overlay.Controls.Add(week);

            week.BringToFront();

            // silence suzuka

            SilenceSuzuka silence = new SilenceSuzuka();
            silence.Location = new Point(week.Right + 10, 30);
            overlay.Controls.Add(silence);

            silence.BringToFront();

            // tokai teio

            TokaiTeio hachimi = new TokaiTeio();
            hachimi.Location = new Point(silence.Right + 10, 30);
            overlay.Controls.Add(hachimi);

            hachimi.BringToFront();

            // maruzensky

            Maruzensky maru = new Maruzensky();
            maru.Location = new Point(hachimi.Right + 10, 30);
            overlay.Controls.Add(maru);

            maru.BringToFront();

            // fuji kiseki

            FujiKiseki kiseki = new FujiKiseki();
            kiseki.Location = new Point(maru.Right + 10, 30);
            overlay.Controls.Add(kiseki);

            kiseki.BringToFront();

            // oguri cap

            OguriCap oguri = new OguriCap();
            oguri.Location = new Point(kiseki.Right + 10, 30);
            overlay.Controls.Add(oguri);

            oguri.BringToFront();

            // gold ship

            GoldShip gold = new GoldShip();
            gold.Location = new Point(oguri.Right + 10, 30);
            overlay.Controls.Add(gold);

            gold.BringToFront();

            // vodka

            Vodka vodka = new Vodka();
            vodka.Location = new Point(gold.Right + 10, 30);
            overlay.Controls.Add(vodka);

            vodka.BringToFront();

            // daiwa scarlet

            DaiwaScarlet daiwa = new DaiwaScarlet();
            daiwa.Location = new Point(vodka.Right + 10, 30);
            overlay.Controls.Add(daiwa);

            daiwa.BringToFront();

            // taiki shuttle
            TaikiShuttle taiki = new TaikiShuttle();
            taiki.Location = new Point(daiwa.Right + 10, 30);
            overlay.Controls.Add(taiki);

            taiki.BringToFront();

            // grass wonder
            GrassWonder grass = new GrassWonder();
            grass.Location = new Point(taiki.Right + 10, 30);
            overlay.Controls.Add(grass);

            grass.BringToFront();

            // hishi amazon
            HishiAmazon hishi = new HishiAmazon();
            hishi.Location = new Point(grass.Right + 10, 30);
            overlay.Controls.Add(hishi);

            hishi.BringToFront();

            // mejiro mcqueen

            MejiroMcQueen mejiro = new MejiroMcQueen();
            mejiro.Location = new Point(40, 100);
            overlay.Controls.Add(mejiro);

            // el condor pasa

            ElCondorPasa pasa = new ElCondorPasa();
            pasa.Location = new Point(mejiro.Right + 10, 100);
            overlay.Controls.Add(pasa);

            pasa.BringToFront();

            // tm opera o
            TMOperaO opera = new TMOperaO();
            opera.Location = new Point(pasa.Right + 10, 100);
            overlay.Controls.Add(opera);

            opera.BringToFront();

            // narita brian
            NaritaBrian brian = new NaritaBrian();
            brian.Location = new Point(opera.Right + 10, 100);
            overlay.Controls.Add(brian);

            brian.BringToFront();

            // symboli rudolf
            SymboliRudolf symboli = new SymboliRudolf();
            symboli.Location = new Point(brian.Right + 10, 100);
            overlay.Controls.Add(symboli);

            symboli.BringToFront();

            // air groove
            AirGroove air = new AirGroove();
            air.Location = new Point(symboli.Right + 10, 100);
            overlay.Controls.Add(air);

            air.BringToFront();

            // seiun sky
            SeiunSky seiun = new SeiunSky();
            seiun.Location = new Point(air.Right + 10, 100);
            overlay.Controls.Add(seiun);

            seiun.BringToFront();

            // tamamo cross
            TamamoCross tamamo = new TamamoCross();
            tamamo.Location = new Point(seiun.Right + 10, 100);
            overlay.Controls.Add(tamamo);

            tamamo.BringToFront();

            // fine motion
            FineMotion fine = new FineMotion();
            fine.Location = new Point(tamamo.Right + 10, 100);
            overlay.Controls.Add(fine);

            fine.BringToFront();

            // biwa hayahide
            BiwaHayahide biwa = new BiwaHayahide();
            biwa.Location = new Point(fine.Right + 10, 100);
            overlay.Controls.Add(biwa);

            biwa.BringToFront();

            // mayano top gun
            MayanoTopGun mayano = new MayanoTopGun();
            mayano.Location = new Point(biwa.Right + 10, 100);
            overlay.Controls.Add(mayano);

            mayano.BringToFront();

            // manhattan cafe
            ManhattanCafe manhattan = new ManhattanCafe();
            manhattan.Location = new Point(mayano.Right + 10, 100);
            overlay.Controls.Add(manhattan);

            manhattan.BringToFront();

            // mihono bourbon
            MihonoBourbon mihono = new MihonoBourbon();
            mihono.Location = new Point(manhattan.Right + 10, 100);
            overlay.Controls.Add(mihono);

            mihono.BringToFront();

            // mejiro ryan
            MejiroRyan ryan = new MejiroRyan();
            ryan.Location = new Point(mihono.Right + 10, 100);
            overlay.Controls.Add(ryan);

            ryan.BringToFront();

            // hishi akebono
            HishiAkebono hishiAkebono = new HishiAkebono();
            hishiAkebono.Location = new Point(ryan.Right + 10, 100);
            overlay.Controls.Add(hishiAkebono);

            hishiAkebono.BringToFront();

            // =========================
            // BACK BUTTON
            // =========================

            BackButton backBtn =
                new BackButton();

            backBtn.Location = new Point(
                (overlay.Width -
                 backBtn.Width) / 2,

                overlay.Height -
                backBtn.Height -
                15
            );

            sfx = new SoundPlayer(Properties.Resources.sfx1);

            backBtn.Click +=
                BackButton_Click;

            overlay.Controls.Add(backBtn);

            backBtn.BringToFront();
        }


        
        // =========================
        // BACK BUTTON
        // =========================

        private void BackButton_Click(
            object sender,
            EventArgs e)
        {
            // Show original Form1
            if (mainForm != null)
            {
                mainForm.Show();
                mainForm.BringToFront();
            }

            // Close PickARacePanel
            this.Close();

            sfx.Play();
        }
    }



    // ==========================================
    // SEMI-TRANSPARENT BLACK PANEL
    // ==========================================

    public class RaceOverlayPanel : Panel
    {
        public RaceOverlayPanel()
        {
            this.BackColor =
                Color.Transparent;

            this.DoubleBuffered = true;
        }

        protected override void OnPaint(
            PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle rect =
                new Rectangle(
                    0,
                    0,
                    this.Width - 1,
                    this.Height - 1
                );

            using (GraphicsPath path =
                new GraphicsPath())
            {
                int radius = 30;

                // TOP LEFT
                path.AddArc(
                    rect.X,
                    rect.Y,
                    radius,
                    radius,
                    180,
                    90
                );

                // TOP RIGHT
                path.AddArc(
                    rect.Right - radius,
                    rect.Y,
                    radius,
                    radius,
                    270,
                    90
                );

                // BOTTOM RIGHT
                path.AddArc(
                    rect.Right - radius,
                    rect.Bottom - radius,
                    radius,
                    radius,
                    0,
                    90
                );

                // BOTTOM LEFT
                path.AddArc(
                    rect.X,
                    rect.Bottom - radius,
                    radius,
                    radius,
                    90,
                    90
                );

                path.CloseFigure();

                // Semi-transparent black
                using (SolidBrush brush =
                    new SolidBrush(
                        Color.FromArgb(
                            160,
                            0,
                            0,
                            0
                        )))
                {
                    e.Graphics.FillPath(
                        brush,
                        path
                    );
                }
            }
        }
    }
}