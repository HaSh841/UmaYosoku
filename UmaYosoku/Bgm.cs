namespace UmaYosoku
{
    // The shared background music of the app (umapyoi).
    // A thin wrapper around LoopingMusic from Audio.cs, so Form1 and
    // PickARacePanel control the same music.
    internal static class Bgm
    {
        private static LoopingMusic music;
        private static bool started = false;
        private static bool paused = false;

        public static void Load()
        {
            if (music != null) return;

            music = new LoopingMusic(Properties.Resources.umapyoi);
        }

        // Start the music (only does something the first time)
        public static void Play()
        {
            if (music == null || started) return;

            started = true;
            paused = false;
            music.Play();
        }

        // Pause: keeps the position
        public static void Pause()
        {
            if (music == null || !started || paused) return;

            paused = true;
            music.Pause();
        }

        // Resume from where it was paused
        public static void Resume()
        {
            if (music == null || !started || !paused) return;

            paused = false;
            music.Resume();
        }

        // Volume from 0 (silent) to 1000 (full)
        public static void SetVolume(int volume)
        {
            if (music == null) return;

            if (volume < 0) volume = 0;
            if (volume > 1000) volume = 1000;
            music.Volume = volume / 1000f;
        }

        // Stop completely (app closing)
        public static void Stop()
        {
            if (music == null) return;

            music.Dispose();
            music = null;
            started = false;
            paused = false;
        }
    }
}