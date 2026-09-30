
using NAudio.CoreAudioApi;
using NAudio.Dsp;
using NAudio.Wave;
using System.Runtime.InteropServices;

namespace NAudioBridge
{
    public class AudioDataEventArgs(float[] waveform, float[] magnitudes, float leftPeak, float rightPeak) : EventArgs
    {
        public float[] Waveform { get; } = waveform;
        public float[] Magnitudes { get; } = magnitudes;
        public float LeftPeak { get; } = leftPeak;
        public float RightPeak { get; } = rightPeak;
    }

    public class WasapiAudioEngine : IDisposable
    {
        private WasapiRecorder? _recorder;
        private const int FftSize = 1024;
        private readonly Complex[] _fftBuffer = new Complex[FftSize];
        private readonly float[] _magnitudes = new float[FftSize / 2];
        private readonly object _lock = new();

        public event EventHandler<AudioDataEventArgs>? AudioDataProcessed;

        public WaveFormat? WaveFormat => _recorder?.WaveFormat;

        public void Start()
        {
            lock (_lock)
            {
                try
                {
                    StopInternal();

                    using var enumerator = new MMDeviceEnumerator();
                    using var renderDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                    if (renderDevice == null || renderDevice.State != DeviceState.Active) return;

                    _recorder = new WasapiRecorderBuilder()
                        .WithDevice(renderDevice)
                        .WithLoopbackCapture()
                        .Build();

                    _recorder.DataAvailable += (buffer, flags, devicePos, qpcPos) =>
                    {
                        try
                        {
                            if (buffer.IsEmpty || _recorder == null) return;
                            byte[] bytes = buffer.ToArray();
                            ProcessBuffer(bytes, _recorder.WaveFormat);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"WASAPI ProcessBuffer Exception: {ex.Message}");
                        }
                    };

                    _recorder.StartRecording();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"WASAPI Loopback start error: {ex.Message}");
                    StopInternal();
                }
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                StopInternal();
            }
        }

        private void StopInternal()
        {
            if (_recorder != null)
            {
                try
                {
                    _recorder.DataAvailable -= null; // Remove handlers
                    _recorder.StopRecording();
                }
                catch { }
                finally
                {
                    _recorder.Dispose();
                    _recorder = null;
                }
            }
        }

        private void ProcessBuffer(byte[] buffer, WaveFormat wf)
        {
            if (buffer == null || buffer.Length == 0) return;

            int channels = wf.Channels > 0 ? wf.Channels : 2;

            // Re-interpret the raw byte buffer as 32-bit floats (standard for WASAPI loopback)
            ReadOnlySpan<float> floatSpan = MemoryMarshal.Cast<byte, float>(buffer);
            int totalSamples = floatSpan.Length;

            if (totalSamples == 0) return;

            float[] samples = new float[totalSamples];
            float leftMax = 0.0f;
            float rightMax = 0.0f;

            for (int i = 0; i < totalSamples; i++)
            {
                float sample = floatSpan[i];
                samples[i] = sample;

                float absVal = Math.Abs(sample);

                // Ignore erratic infinity/NaN values
                if (float.IsNaN(absVal) || float.IsInfinity(absVal)) continue;

                if (channels >= 2)
                {
                    if (i % 2 == 0)
                        leftMax = Math.Max(leftMax, absVal);
                    else
                        rightMax = Math.Max(rightMax, absVal);
                }
                else
                {
                    leftMax = Math.Max(leftMax, absVal);
                    rightMax = leftMax;
                }
            }

            // Perform FFT for Visualizers
            for (int i = 0; i < FftSize; i++)
            {
                if (i < samples.Length)
                {
                    _fftBuffer[i].X = samples[i];
                    _fftBuffer[i].Y = 0;
                }
                else
                {
                    _fftBuffer[i].X = 0;
                    _fftBuffer[i].Y = 0;
                }
            }

            FastFourierTransform.FFT(true, (int)Math.Log(FftSize, 2), _fftBuffer);

            for (int i = 0; i < _magnitudes.Length; i++)
            {
                _magnitudes[i] = (float)Math.Sqrt(_fftBuffer[i].X * _fftBuffer[i].X + _fftBuffer[i].Y * _fftBuffer[i].Y);
            }

            // Raise Event for VB.NET
            AudioDataProcessed?.Invoke(this, new AudioDataEventArgs(samples, _magnitudes, leftMax, rightMax));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stop();
            }
        }
    }
}
