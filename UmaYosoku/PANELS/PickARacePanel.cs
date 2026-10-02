using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
using UmaYosoku.Button;
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

            // yukino bijin
            YukinoBijin yukino = new YukinoBijin();
            yukino.Location = new Point(40, 170);
            overlay.Controls.Add(yukino);

            yukino.BringToFront();

            // rice shower
            RiceShower riceShower = new RiceShower();
            riceShower.Location = new Point(yukino.Right + 10, 170);
            overlay.Controls.Add(riceShower);

            riceShower.BringToFront();

            // ines fujin
            InesFujin inesFujin = new InesFujin();
            inesFujin.Location = new Point(riceShower.Right + 10, 170);
            overlay.Controls.Add(inesFujin);

            inesFujin.BringToFront();

            // agnes tachyon
            AgnesTachyon agnesTachyon = new AgnesTachyon();
            agnesTachyon.Location = new Point(inesFujin.Right + 10, 170);
            overlay.Controls.Add(agnesTachyon);

            agnesTachyon.BringToFront();

            // inari one
            InariOne inariOne = new InariOne();
            inariOne.Location = new Point(agnesTachyon.Right + 10, 170);
            overlay.Controls.Add(inariOne);

            inariOne.BringToFront();

            // winning ticket
            WinningTicket winningTicket = new WinningTicket();
            winningTicket.Location = new Point(inariOne.Right + 10, 170);
            overlay.Controls.Add(winningTicket);

            winningTicket.BringToFront();

            // air shakur
            AirShakur airShakur = new AirShakur();
            airShakur.Location = new Point(winningTicket.Right + 10, 170);
            overlay.Controls.Add(airShakur);

            airShakur.BringToFront();

            // eishin flash
            EishinFlash eishinFlash = new EishinFlash();
            eishinFlash.Location = new Point(airShakur.Right + 10, 170);
            overlay.Controls.Add(eishinFlash);

            eishinFlash.BringToFront();

            // curren chan
            CurrenChan currenChan = new CurrenChan();
            currenChan.Location = new Point(eishinFlash.Right + 10, 170);
            overlay.Controls.Add(currenChan);

            currenChan.BringToFront();

            // kawakami princess
            KawakamiPrincess kawakamiPrincess = new KawakamiPrincess();
            kawakamiPrincess.Location = new Point(currenChan.Right + 10, 170);
            overlay.Controls.Add(kawakamiPrincess);

            kawakamiPrincess.BringToFront();

            // gold city
            GoldCity goldCity = new GoldCity();
            goldCity.Location = new Point(kawakamiPrincess.Right + 10, 170);
            overlay.Controls.Add(goldCity);

            goldCity.BringToFront();

            // sakura bakushin o
            SakuraBakushinO sakuraBakushinO = new SakuraBakushinO();
            sakuraBakushinO.Location = new Point(goldCity.Right + 10, 170);
            overlay.Controls.Add(sakuraBakushinO);

            sakuraBakushinO.BringToFront();

            // seeking the pearl
            SeekingThePearl seekingThePearl = new SeekingThePearl();
            seekingThePearl.Location = new Point(sakuraBakushinO.Right + 10, 170);
            overlay.Controls.Add(seekingThePearl);

            seekingThePearl.BringToFront();

            // shinko windy
            ShinkoWindy shinkoWindy = new ShinkoWindy();
            shinkoWindy.Location = new Point(seekingThePearl.Right + 10, 170);
            overlay.Controls.Add(shinkoWindy);

            shinkoWindy.BringToFront();

            // sweep tosho
            SweepTosho sweepTosho = new SweepTosho();
            sweepTosho.Location = new Point(shinkoWindy.Right + 10, 170);
            overlay.Controls.Add(sweepTosho);

            sweepTosho.BringToFront();

            // super creek
            SuperCreek superCreek = new SuperCreek();
            superCreek.Location = new Point(40, 240);
            overlay.Controls.Add(superCreek);

            superCreek.BringToFront();

            // smart falcon
            SmartFalcon smartFalcon = new SmartFalcon();
            smartFalcon.Location = new Point(superCreek.Right + 10, 240);
            overlay.Controls.Add(smartFalcon);

            smartFalcon.BringToFront();

            // zenno rob roy
            ZennoRobRoy zennoRobRoy = new ZennoRobRoy();
            zennoRobRoy.Location = new Point(smartFalcon.Right + 10, 240);
            overlay.Controls.Add(zennoRobRoy);

            zennoRobRoy.BringToFront();

            // tosen jordan
            TosenJordan tosenJordan = new TosenJordan();
            tosenJordan.Location = new Point(zennoRobRoy.Right + 10, 240);
            overlay.Controls.Add(tosenJordan);

            tosenJordan.BringToFront();

            // nakayama festa
            NakayamaFesta nakayamaFesta = new NakayamaFesta();
            nakayamaFesta.Location = new Point(tosenJordan.Right + 10, 240);
            overlay.Controls.Add(nakayamaFesta);

            nakayamaFesta.BringToFront();

            // narita taishin
            NaritaTaishin naritaTaishin = new NaritaTaishin();
            naritaTaishin.Location = new Point(nakayamaFesta.Right + 10, 240);
            overlay.Controls.Add(naritaTaishin);

            naritaTaishin.BringToFront();

            // nishino flower
            NishinoFlower nishinoFlower = new NishinoFlower();
            nishinoFlower.Location = new Point(naritaTaishin.Right + 10, 240);
            overlay.Controls.Add(nishinoFlower);

            nishinoFlower.BringToFront();

            // haru urara
            HaruUrara haruurara = new HaruUrara();
            haruurara.Location = new Point(nishinoFlower.Right + 10, 240);
            overlay.Controls.Add(haruurara);

            haruurara.BringToFront();

            // bamboo memory
            BambooMemory bambooMemory = new BambooMemory();
            bambooMemory.Location = new Point(haruurara.Right + 10, 240);
            overlay.Controls.Add(bambooMemory);

            bambooMemory.BringToFront();

            // biko pegasus
            BikoPegasus bikoPegasus = new BikoPegasus();
            bikoPegasus.Location = new Point(bambooMemory.Right + 10, 240);
            overlay.Controls.Add(bikoPegasus);

            bikoPegasus.BringToFront();

            // marvelous sunday
            MarvelousSunday marvelousSunday = new MarvelousSunday();
            marvelousSunday.Location = new Point(bikoPegasus.Right + 10, 240);
            overlay.Controls.Add(marvelousSunday);

            marvelousSunday.BringToFront();

            // matikanefukukitaru
            Matikanefukukitaru matikanefukukitaru = new Matikanefukukitaru();
            matikanefukukitaru.Location = new Point(marvelousSunday.Right + 10, 240);
            overlay.Controls.Add(matikanefukukitaru);

            matikanefukukitaru.BringToFront();

            // mr cb
            MrCB mrCB = new MrCB();
            mrCB.Location = new Point(matikanefukukitaru.Right + 10, 240);
            overlay.Controls.Add(mrCB);

            mrCB.BringToFront();

            // meisho doto
            MeishoDoto meishoDoto = new MeishoDoto();
            meishoDoto.Location = new Point(mrCB.Right + 10, 240);
            overlay.Controls.Add(meishoDoto);

            meishoDoto.BringToFront();

            // mejiro dober
            MejiroDober mejiroDober = new MejiroDober();
            mejiroDober.Location = new Point(meishoDoto.Right + 10, 240);
            overlay.Controls.Add(mejiroDober);

            mejiroDober.BringToFront();

            // nice nature
            NiceNature niceNature = new NiceNature();
            niceNature.Location = new Point(40, 310);
            overlay.Controls.Add(niceNature);

            niceNature.BringToFront();

            // king halo
            KingHalo kingHalo = new KingHalo();
            kingHalo.Location = new Point(niceNature.Right + 10, 310);
            overlay.Controls.Add(kingHalo);

            kingHalo.BringToFront();

            // mantikanetannhauser
            Matikanetannhauser matikanetannhauser = new Matikanetannhauser();
            matikanetannhauser.Location = new Point(kingHalo.Right + 10, 310);
            overlay.Controls.Add(matikanetannhauser);

            matikanetannhauser.BringToFront();

            // ikuno dictus
            IkunoDictus ikunoDictus = new IkunoDictus();
            ikunoDictus.Location = new Point(matikanetannhauser.Right + 10, 310);
            overlay.Controls.Add(ikunoDictus);

            ikunoDictus.BringToFront();

            // mejiro palmer
            MejiroPalmer mejiroPalmer = new MejiroPalmer();
            mejiroPalmer.Location = new Point(ikunoDictus.Right + 10, 310);
            overlay.Controls.Add(mejiroPalmer);

            mejiroPalmer.BringToFront();

            // daitaku helios
            DaitakuHelios daitakuHelios = new DaitakuHelios();
            daitakuHelios.Location = new Point(mejiroPalmer.Right + 10, 310);
            overlay.Controls.Add(daitakuHelios);

            daitakuHelios.BringToFront();

            // twin turbo
            TwinTurbo twinTurbo = new TwinTurbo();
            twinTurbo.Location = new Point(daitakuHelios.Right + 10, 310);
            overlay.Controls.Add(twinTurbo);

            twinTurbo.BringToFront();

            // satono diamond
            SatonoDiamond satonoDiamond = new SatonoDiamond();
            satonoDiamond.Location = new Point(twinTurbo.Right + 10, 310);
            overlay.Controls.Add(satonoDiamond);

            satonoDiamond.BringToFront();

            // kitasan black
            KitasanBlack kitasanBlack = new KitasanBlack();
            kitasanBlack.Location = new Point(satonoDiamond.Right + 10, 310);
            overlay.Controls.Add(kitasanBlack);

            kitasanBlack.BringToFront();

            // sakura chiyono o
            SakuraChiyonoO sakuraChiyonoO = new SakuraChiyonoO();
            sakuraChiyonoO.Location = new Point(kitasanBlack.Right + 10, 310);
            overlay.Controls.Add(sakuraChiyonoO);

            sakuraChiyonoO.BringToFront();

            // sirius symboli
            SiriusSymboli siriusSymboli = new SiriusSymboli();
            siriusSymboli.Location = new Point(sakuraChiyonoO.Right + 10, 310);
            overlay.Controls.Add(siriusSymboli);

            siriusSymboli.BringToFront();

            // mejiro ardan
            MejiroArdan mejiroArdan = new MejiroArdan();
            mejiroArdan.Location = new Point(siriusSymboli.Right + 10, 310);
            overlay.Controls.Add(mejiroArdan);

            mejiroArdan.BringToFront();

            // yaeno muteki
            YaenoMuteki yaenoMuteki = new YaenoMuteki();
            yaenoMuteki.Location = new Point(mejiroArdan.Right + 10, 310);
            overlay.Controls.Add(yaenoMuteki);

            yaenoMuteki.BringToFront();

            // tsurumaru tsuyoshi
            TsurumaruTsuyoshi tsurumaruTsuyoshi = new TsurumaruTsuyoshi();
            tsurumaruTsuyoshi.Location = new Point(yaenoMuteki.Right + 10, 310);
            overlay.Controls.Add(tsurumaruTsuyoshi);

            tsurumaruTsuyoshi.BringToFront();

            // mejiro bright
            MejiroBright mejiroBright = new MejiroBright();
            mejiroBright.Location = new Point(tsurumaruTsuyoshi.Right + 10, 310);
            overlay.Controls.Add(mejiroBright);

            mejiroBright.BringToFront();

            // sakura laurel
            SakuraLaurel sakuraLaurel = new SakuraLaurel();
            sakuraLaurel.Location = new Point(40, 380);
            overlay.Controls.Add(sakuraLaurel);

            sakuraLaurel.BringToFront();

            // narita top road
            NaritaTopRoad naritaTopRoad = new NaritaTopRoad();
            naritaTopRoad.Location = new Point(sakuraLaurel.Right + 10, 380);
            overlay.Controls.Add(naritaTopRoad);

            naritaTopRoad.BringToFront();

            // yamanin zephyr
            YamaninZephyr yamaninZephyr = new YamaninZephyr();
            yamaninZephyr.Location = new Point(naritaTopRoad.Right + 10, 380);
            overlay.Controls.Add(yamaninZephyr);

            yamaninZephyr.BringToFront();

            // symboli kris s
            SymboliKrisS symboliKrisS = new SymboliKrisS();
            symboliKrisS.Location = new Point(yamaninZephyr.Right + 10, 380);
            overlay.Controls.Add(symboliKrisS);

            symboliKrisS.BringToFront();

            // tanino gimlet
            TaninoGimlet taninoGimlet = new TaninoGimlet();
            taninoGimlet.Location = new Point(symboliKrisS.Right + 10, 380);
            overlay.Controls.Add(taninoGimlet);

            taninoGimlet.BringToFront();

            // daiichi ruby
            DaiichiRuby daiichiRuby = new DaiichiRuby();
            daiichiRuby.Location = new Point(taninoGimlet.Right + 10, 380);
            overlay.Controls.Add(daiichiRuby);

            daiichiRuby.BringToFront();

            // aston machan
            AstonMachan astonMachan = new AstonMachan();
            astonMachan.Location = new Point(daiichiRuby.Right + 10, 380);
            overlay.Controls.Add(astonMachan);

            astonMachan.BringToFront();

            // k s miracle
            KSMiracle kSMiracle = new KSMiracle();
            kSMiracle.Location = new Point(astonMachan.Right + 10, 380);
            overlay.Controls.Add(kSMiracle);

            kSMiracle.BringToFront();

            // copano rickey
            CopanoRickey copanoRickey = new CopanoRickey();
            copanoRickey.Location = new Point(kSMiracle.Right + 10, 380);
            overlay.Controls.Add(copanoRickey);

            copanoRickey.BringToFront();

            // wonder acute
            WonderAcute wonderAcute = new WonderAcute();
            wonderAcute.Location = new Point(copanoRickey.Right + 10, 380);
            overlay.Controls.Add(wonderAcute);

            wonderAcute.BringToFront();

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