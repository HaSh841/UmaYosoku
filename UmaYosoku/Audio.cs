using System;
using System.IO;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

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

        // Like PlaySfx, but you start it yourself (Play) and can wait until
        // it finishes (Finished). It also tells you how long it is (DurationMs).
        // Used for the intro voice and the exit sound.
        public static SfxClip OpenClip(Stream resource)
        {
            byte[] data = ReadAll(resource);
            return new SfxClip(new WaveFileReader(new MemoryStream(data)));
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
    // ONE SOUND YOU CAN WAIT FOR
    // =========================

    public sealed class SfxClip : IDisposable
    {
        private readonly WaveFileReader reader;
        private readonly WaveOutEvent output;
        private readonly TaskCompletionSource<bool> done =
            new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        // Length of the sound in milliseconds
        public int DurationMs { get; }

        // Completes when the sound has finished playing
        public Task Finished => done.Task;

        public SfxClip(WaveFileReader reader)
        {
            this.reader = reader;
            DurationMs = (int)reader.TotalTime.TotalMilliseconds;

            output = new WaveOutEvent();
            output.PlaybackStopped += (s, e) => done.TrySetResult(true);
            output.Init(reader);

            // Reset the app's Windows volume to full (it can get stuck at 0)
            output.Volume = 1f;
        }

        public void Play()
        {
            output.Play();
        }

        public void Dispose()
        {
            output.Dispose();
            reader.Dispose();
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
        private readonly VolumeSampleProvider volumeProvider;
        private readonly WaveOutEvent output;

        public LoopingMusic(Stream resource)
        {
            MemoryStream ms = new MemoryStream();
            resource.CopyTo(ms);
            ms.Position = 0;

            reader = new WaveFileReader(ms);
            loop = new LoopStream(reader);

            // The music's volume is changed INSIDE the stream (volumeProvider),
            // not with output.Volume, because output.Volume changes the whole
            // app's volume in the Windows Volume Mixer and Windows remembers it.
            volumeProvider = new VolumeSampleProvider(loop.ToSampleProvider());

            output = new WaveOutEvent();
            output.Init(new SampleToWaveProvider(volumeProvider));

            // Reset the app's Windows volume to full (it can get stuck at 0)
            output.Volume = 1f;
        }

        public void Play()
        {
            output.Play();
        }

        // Pause keeps the position, Resume continues from the same spot
        public void Pause()
        {
            output.Pause();
        }

        public void Resume()
        {
            output.Play();
        }

        // Volume from 0.0 (silent) to 1.0 (full)
        public float Volume
        {
            get => volumeProvider.Volume;
            set => volumeProvider.Volume = value;
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