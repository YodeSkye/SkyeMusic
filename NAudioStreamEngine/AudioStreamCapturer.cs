using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using NAudio.Wave;
using NAudio.Lame;

namespace NAudioStreamEngine
{
    public static class AudioStreamCapturer
    {
        private static WasapiRecorder? _recorder;
        private static LameMP3FileWriter? _mp3Writer;
        private static bool _isCapturing;
        private static readonly Lock _captureLock = new();

        public static event Action<byte[], int>? AudioDataCaptured;

        public static void StartCapture(int targetBitrateKbps)
        {
            lock (_captureLock)
            {
                if (_isCapturing) return;

                try
                {
                    _recorder = new WasapiRecorderBuilder()
                        .WithLoopbackCapture()
                        .WithLowLatency()
                        .Build();

                    var bridgeStream = new Mp3CallbackStream((buffer, offset, count) =>
                    {
                        byte[] chunk = new byte[count];
                        Array.Copy(buffer, offset, chunk, 0, count);
                        AudioDataCaptured?.Invoke(chunk, count);
                    });

                    // Create a standard 16-bit PCM format that LAME requires, matching sample rate & channels
                    var standardPcmFormat = new WaveFormat(_recorder.WaveFormat.SampleRate, 16, _recorder.WaveFormat.Channels);
                    _mp3Writer = new LameMP3FileWriter(bridgeStream, standardPcmFormat, targetBitrateKbps);

                    // NAudio 3 WasapiRecorder DataAvailable delegate signature: 
                    // (ReadOnlySpan<byte> buffer, AudioClientBufferFlags flags, long devicePosition, long qpcPosition)
                    _recorder.DataAvailable += (bufferSpan, flags, devicePos, qpcPos) =>
                    {
                        if (_mp3Writer != null && bufferSpan.Length > 0)
                        {
                            byte[] pcmBytes;

                            // Convert 32-bit float / extensible hardware format to standard 16-bit PCM bytes for LAME
                            if (_recorder.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat ||
                                _recorder.WaveFormat.Encoding == WaveFormatEncoding.Extensible)
                            {
                                var floats = MemoryMarshal.Cast<byte, float>(bufferSpan);
                                pcmBytes = new byte[floats.Length * 2];
                                for (int i = 0; i < floats.Length; i++)
                                {
                                    float sample = floats[i];
                                    if (sample > 1.0f) sample = 1.0f;
                                    if (sample < -1.0f) sample = -1.0f;

                                    short pcmValue = (short)(sample * 32767);
                                    byte[] bytes = BitConverter.GetBytes(pcmValue);
                                    pcmBytes[i * 2] = bytes[0];
                                    pcmBytes[i * 2 + 1] = bytes[1];
                                }
                            }
                            else
                            {
                                pcmBytes = bufferSpan.ToArray();
                            }

                            _mp3Writer.Write(pcmBytes, 0, pcmBytes.Length);
                        }
                    };

                    _recorder.RecordingStopped += (s, e) => StopCapture();

                    _recorder.StartRecording();
                    _isCapturing = true;

                    Debug.WriteLine($"[AUDIOSTREAMCAPTURER] WASAPI + MP3 encoder started at {targetBitrateKbps} kbps.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[AUDIOSTREAMCAPTURER] Failed to start recorder/encoder: {ex.Message}");
                    StopCapture();
                }
            }
        }

        public static void StopCapture()
        {
            lock (_captureLock)
            {
                if (!_isCapturing && _recorder == null) return;

                try
                {
                    if (_recorder != null)
                    {
                        _recorder.StopRecording();
                        _recorder.Dispose();
                        _recorder = null;
                    }

                    if (_mp3Writer != null)
                    {
                        _mp3Writer.Flush();
                        _mp3Writer.Dispose();
                        _mp3Writer = null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[AUDIOSTREAMCAPTURER] Error stopping capture: {ex.Message}");
                }
                finally
                {
                    _isCapturing = false;
                    Debug.WriteLine("[AUDIOSTREAMCAPTURER] Engine stopped.");
                }
            }
        }
    }

    internal class Mp3CallbackStream(Action<byte[], int, int> onWrite) : Stream
    {
        private readonly Action<byte[], int, int> _onWrite = onWrite;

        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override void Write(byte[] buffer, int offset, int count) => _onWrite(buffer, offset, count);
        public override void Flush() { }
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
    }
}