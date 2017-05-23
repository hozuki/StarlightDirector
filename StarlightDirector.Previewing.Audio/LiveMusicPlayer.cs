using System;
using System.Collections.Generic;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using StarlightDirector.Core;
using AudioOut = NAudio.Wave.WasapiOut;

namespace StarlightDirector.Previewing.Audio {
    public sealed class LiveMusicPlayer : DisposableBase {

        public LiveMusicPlayer() {
            _syncObject = new object();
            _waveStream = new WaveMixerStream32();
            _soundPlayer = new AudioOut(AudioClientShareMode.Shared, 60);
            _soundPlayer.Init(_waveStream);
            _channels = new Dictionary<WaveStream, WaveChannel32>();
            PreviewingSettings.MusicVolumeChanged += OnMusicVolumeChanged;
        }

        public event EventHandler<StoppedEventArgs> PlaybackStopped {
            add {
                if (_soundPlayer != null) {
                    _soundPlayer.PlaybackStopped += value;
                }
            }
            remove {
                if (_soundPlayer != null) {
                    _soundPlayer.PlaybackStopped -= value;
                }
            }
        }

        public event EventHandler<EventArgs> PositionChanged;

        public void Play() {
            if (IsPlaying) {
                if (IsPaused) {
                    IsPaused = false;
                } else {
                    Stop();
                }
            }
            _soundPlayer?.Play();
            IsPlaying = true;
        }

        public void Stop() {
            _soundPlayer?.Stop();
            IsPlaying = false;
        }

        public void Pause() {
            if (!IsPlaying || IsPaused) {
                return;
            }
            _soundPlayer?.Pause();
            IsPaused = true;
        }

        public TimeSpan CurrentTime {
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
                PositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public TimeSpan TotalTime => _waveStream.TotalTime;

        public bool IsPlaying {
            get {
                lock (_syncObject) {
                    return _isPlaying;
                }
            }
            private set {
                lock (_syncObject) {
                    _isPlaying = value;
                }
            }
        }

        public bool IsPaused {
            get {
                lock (_syncObject) {
                    return _isPaused;
                }
            }
            private set {
                lock (_syncObject) {
                    _isPaused = value;
                }
            }
        }

        public bool HasMusicInputStream => _musicChannel != null;

        public WaveChannel32 AddMusicInputStream(WaveStream waveStream, float volume = 1f) {
            if (_musicChannel != null) {
                RemoveMusicInputStream();
            }
            lock (_syncObject) {
                var rateConvertedStream = waveStream;
                if (NeedSampleRateConversion(waveStream.WaveFormat)) {
                    rateConvertedStream = new ResamplerDmoStream(waveStream, _waveStream.WaveFormat);
                }
                var addedStream = new WaveChannel32(rateConvertedStream, volume, 0f);
                _musicChannel = addedStream;
                _originalMusicWaveStream = waveStream;
                _waveStream.AddInputStream(addedStream);
                return addedStream;
            }
        }

        public WaveChannel32 AddSfxInputStream(WaveStream waveStream, float volume = 1f) {
            lock (_syncObject) {
                var rateConvertedStream = waveStream;
                if (NeedSampleRateConversion(waveStream.WaveFormat)) {
                    rateConvertedStream = new ResamplerDmoStream(waveStream, _waveStream.WaveFormat);
                }
                var addedStream = new WaveChannel32(rateConvertedStream, volume, 0f);
                if (!_channels.ContainsKey(waveStream)) {
                    _channels.Add(waveStream, addedStream);
                }
                _waveStream.AddInputStream(addedStream);
                return addedStream;
            }
        }

        public void RemoveMusicInputStream() {
            lock (_syncObject) {
                _waveStream.RemoveInputStream(_musicChannel);
                _musicChannel.Dispose();
                _musicChannel = null;
            }
        }

        public void RemoveSfxInputStream(WaveStream waveStream) {
            lock (_syncObject) {
                if (_channels.ContainsKey(waveStream)) {
                    _channels.Remove(waveStream);
                }
                _waveStream.RemoveInputStream(waveStream);
            }
        }

        protected override void Dispose(bool disposing) {
            PreviewingSettings.MusicVolumeChanged -= OnMusicVolumeChanged;
            _soundPlayer?.Stop();
            _soundPlayer?.Dispose();
            _waveStream?.Dispose();
            _soundPlayer = null;
            _waveStream = null;
            _soundPlayer = null;
        }

        private bool NeedSampleRateConversion(WaveFormat waveFormat) {
            if (_waveStream.InputCount == 0) {
                return false;
            }
            return waveFormat.SampleRate != _waveStream.WaveFormat.SampleRate;
        }

        private void OnMusicVolumeChanged(object sender, EventArgs e) {
            _musicChannel.Volume = PreviewingSettings.MusicVolume;
        }

        private WaveMixerStream32 _waveStream;
        private AudioOut _soundPlayer;
        private bool _isPlaying;
        private bool _isPaused;
        private readonly object _syncObject;
        private readonly Dictionary<WaveStream, WaveChannel32> _channels;
        private WaveStream _originalMusicWaveStream;
        private WaveChannel32 _musicChannel;

    }
}