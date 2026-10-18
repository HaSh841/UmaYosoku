using System;
using System.IO;
using NAudio.Wave;

namespace UmaYosoku
{
    // =========================
    // ONE-SHOT SOUND EFFECTS
    // Each call gets its own output, so sounds overlap with each other
    // and with the BGM, and keep playing even after a form closes.
    // =========================

    public static class Audio
    {
        public static void PlaySfx(Stream resource)
        {
            byte[] data = ReadAll(resource);

            WaveFileReader reader = new WaveFileReader(new MemoryStream(data));
            WaveOutEvent output = new WaveOutEvent();

            output.Init(reader);

            output.PlaybackStopped += (s, e) =>
            {
                output.Dispose();
                reader.Dispose();
            };

            output.Play();
        }

        private static byte[] ReadAll(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }

    // =========================
    // LOOPING BACKGROUND MUSIC
    // Stop() only stops THIS music, never the sound effects.
    // =========================

    public class LoopingMusic : IDisposable
    {
        private readonly WaveFileReader reader;
        private readonly LoopStream loop;
        private readonly WaveOutEvent output;

        public LoopingMusic(Stream resource)
        {
            MemoryStream ms = new MemoryStream();
            resource.CopyTo(ms);
            ms.Position = 0;

            reader = new WaveFileReader(ms);
            loop = new LoopStream(reader);

            output = new WaveOutEvent();
            output.Init(loop);
        }

        public void Play()
        {
            output.Play();
        }

        public void Stop()
        {
            output.Stop();
        }

        public void Dispose()
        {
            output.Stop();
            output.Dispose();
            reader.Dispose();
        }
    }

    // Wraps a wave stream and rewinds it when it reaches the end
    internal class LoopStream : WaveStream
    {
        private readonly WaveStream source;

        public LoopStream(WaveStream source)
        {
            this.source = source;
        }

        public override WaveFormat WaveFormat => source.WaveFormat;

        public override long Length => source.Length;

        public override long Position
        {
            get => source.Position;
            set => source.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int total = 0;

            while (total < count)
            {
                int read = source.Read(buffer, offset + total, count - total);

                if (read == 0)
                {
                    if (source.Position == 0)
                    {
                        break; // empty file, avoid an infinite loop
                    }

                    source.Position = 0;
                }
                else
                {
                    total += read;
                }
            }

            return total;
        }
    }
}