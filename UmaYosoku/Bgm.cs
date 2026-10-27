using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace UmaYosoku
{
    // Shared background music for the whole app.
    // Uses the Windows MCI audio API (winmm.dll), so it can PAUSE and RESUME
    // from the same spot, and other SoundPlayer sounds (button clicks,
    // character music) don't cut it off. No extra references needed.
    internal static class Bgm
    {
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern int mciSendString(
            string command, StringBuilder returnValue, int returnLength, IntPtr callback);

        private const string Alias = "umabgm";

        private static string tempPath;
        private static bool loaded = false;
        private static bool started = false;
        private static bool paused = false;
        private static System.Windows.Forms.Timer loopTimer;

        public static void Load()
        {
            if (loaded) return;

            // MCI needs a real file, so copy the embedded wav to temp
            tempPath = Path.Combine(Path.GetTempPath(), "umayosoku_bgm.wav");
            using (Stream src = Properties.Resources.umapyoi)
            using (FileStream fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            {
                if (src.CanSeek) src.Position = 0;
                src.CopyTo(fs);
            }

            mciSendString("close " + Alias, null, 0, IntPtr.Zero);
            mciSendString(
                "open \"" + tempPath + "\" type waveaudio alias " + Alias,
                null, 0, IntPtr.Zero);

            // Loop: when the song ends, start it again
            loopTimer = new System.Windows.Forms.Timer();
            loopTimer.Interval = 250;
            loopTimer.Tick += (s, e) =>
            {
                if (!started || paused) return;

                StringBuilder mode = new StringBuilder(32);
                mciSendString("status " + Alias + " mode", mode, mode.Capacity, IntPtr.Zero);

                if (mode.ToString() == "stopped")
                {
                    mciSendString("play " + Alias + " from 0", null, 0, IntPtr.Zero);
                }
            };

            loaded = true;
        }

        // Start the music (only does something the first time)
        public static void Play()
        {
            if (!loaded || started) return;

            started = true;
            paused = false;
            mciSendString("play " + Alias + " from 0", null, 0, IntPtr.Zero);
            loopTimer.Start();
        }

        // Pause: keeps the position
        public static void Pause()
        {
            if (!loaded || !started || paused) return;

            paused = true;
            mciSendString("pause " + Alias, null, 0, IntPtr.Zero);
        }

        // Resume from where it was paused
        public static void Resume()
        {
            if (!loaded || !started || !paused) return;

            paused = false;
            mciSendString("resume " + Alias, null, 0, IntPtr.Zero);
        }

        // Stop completely (app closing)
        public static void Stop()
        {
            if (!loaded) return;

            loopTimer?.Stop();
            loopTimer?.Dispose();
            loopTimer = null;

            mciSendString("stop " + Alias, null, 0, IntPtr.Zero);
            mciSendString("close " + Alias, null, 0, IntPtr.Zero);

            loaded = false;
            started = false;
            paused = false;

            try { if (tempPath != null) File.Delete(tempPath); } catch { }
        }
    }
}