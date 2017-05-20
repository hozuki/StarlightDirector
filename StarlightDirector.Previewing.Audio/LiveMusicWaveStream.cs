using System;
using System.IO;
using NAudio.Wave;

namespace StarlightDirector.Previewing.Audio {
    public sealed class LiveMusicWaveStream : WaveStream {
        
        public static LiveMusicWaveStream FromWaveStream(Stream stream) {
            return new LiveMusicWaveStream(stream);
        }

        public override TimeSpan CurrentTime {
            get => _waveStream.CurrentTime;
            set {
                lock (_syncObject) {
                    var waveStream = _waveStream;
                    waveStream.CurrentTime = value;
                    var position = waveStream.Position;
                    var blockAlign = waveStream.BlockAlign;
                    if (position % blockAlign != 0) {
                        position = (long)(Math.Round(position / (double)blockAlign) * blockAlign);
                        if (position < 0) {
                            position = 0;
                        }
                        waveStream.Position = position;
                    }
                }
            }
        }

        public Stream SourceStream => _sourceStream;

        public override TimeSpan TotalTime => _waveStream.TotalTime;

        public override long Length => _waveStream.Length;

        public override long Position {
            get => _waveStream.Position;
            set {
                lock (_syncObject) {
                    var waveStream = _waveStream;
                    var position = value;
                    var blockAlign = waveStream.BlockAlign;
                    if (position % blockAlign != 0) {
                        position = (long)(Math.Round(position / (double)blockAlign) * blockAlign);
                        if (position < 0) {
                            position = 0;
                        }
                    }
                    waveStream.Position = position;
                }
            }
        }

        public override WaveFormat WaveFormat => _waveStream.WaveFormat;

        public override int Read(byte[] buffer, int offset, int count) {
            lock (_syncObject) {
                return _waveStream.Read(buffer, offset, count);
            }
        }

        protected override void Dispose(bool disposing) {
            if (_disposed) {
                return;
            }
            if (disposing) {
                _waveStream?.Dispose();
                _hcaDataStream?.Dispose();
                _waveStream = null;
                _hcaDataStream = null;
            }
            base.Dispose(disposing);
            _disposed = true;
        }

        private LiveMusicWaveStream(Stream waveStream) {
            _waveStream = new WaveFileReader(waveStream);
            _sourceStream = waveStream;
        }

        private bool _disposed;

        private readonly Stream _sourceStream;
        private Stream _hcaDataStream;
        private WaveStream _waveStream;
        private readonly object _syncObject = new object();

    }
}