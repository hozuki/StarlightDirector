using System;
using System.Diagnostics;
using System.IO;
using System.Timers;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.DirectorApplication;
using OpenCGSS.StarlightDirector.Globalization;
using OpenCGSS.StarlightDirector.Models;
using OpenCGSS.StarlightDirector.Previewing.Audio;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain {

        private sealed class LiveControl : DisposableBase {

            internal LiveControl() {
                _audioManager = new AudioManager();
                _liveSfxManager = new SfxManager(_audioManager);
            }

            public event EventHandler<EventArgs> Tick;

            public void PreloadWave([NotNull] string fileName) {
                _liveSfxManager.PreloadWave(fileName);
            }

            public void Load([NotNull] string musicFileName, [CanBeNull] out string errorMessageTemplate) {
                errorMessageTemplate = null;

                if (File.Exists(musicFileName)) {
                    try {
                        _liveFileStream = File.Open(musicFileName, FileMode.Open, FileAccess.Read);
                        _liveWaveStream = LiveMusicWaveStream.FromWaveStream(_liveFileStream);

                        _liveMusicPlayer = new LiveMusicPlayer(_audioManager);
                        _liveMusicPlayer.LoadStream(_liveWaveStream);
                    } catch (IOException) {
                        errorMessageTemplate = LanguageManager.TryGetString("messages.fmain.live_music_open_error_template") ?? "Cannot open '{0}'. Live music disabled.";
                    }
                } else {
                    errorMessageTemplate = LanguageManager.TryGetString("messages.fmain.live_music_not_found_template") ?? "File '{0}' does not exist. Live music disabled.";
                }
            }

            public void Start() {
                StartFromTime(0);
            }

            public void StartFromTime(double startTime) {
                var currentDirectorSettings = DirectorSettingsManager.CurrentSettings;
                var frameInterval = 0; // target frame interval in milliseconds

                if (_liveTimer == null) {
                    if (currentDirectorSettings.PreviewTimerInterval > 0) {
                        frameInterval = currentDirectorSettings.PreviewTimerInterval;
                    } else {
                        var refreshRate = ScreenHelper.GetCurrentRefreshRate();

                        if (refreshRate == 0) {
                            // If this method fails, we fall back to the default refresh rate.
                            refreshRate = 60;
                        }

                        // Why is there "-1"?
                        frameInterval = (int)(1000f / refreshRate) - 1;
                    }

                    _liveTimer = new Timer(frameInterval);
                    _liveTimer.Elapsed += LiveTimer_Elapsed;
                }

                _synchronizationMode = currentDirectorSettings.PreviewTimingSynchronizationMode;

                _lastStopwatchDifference = TimeSpan.Zero;

                _liveStartTime = TimeSpan.FromSeconds(startTime);

                if (_liveMusicPlayer != null) {
                    _liveMusicPlayer.CurrentTime = _liveStartTime;
                }

                SfxBufferTime = startTime - frameInterval / 1000.0f;

                _liveStopwatch.Start();
                _liveMusicPlayer?.Play();
                _liveTimer.Start();
            }

            public void Stop() {
                _liveTimer?.Stop();
                _liveMusicPlayer?.Stop();
                if (_liveWaveStream != null) {
                    _liveMusicPlayer?.Stop();
                    _liveMusicPlayer?.Dispose();
                    _liveWaveStream?.Dispose();
                    _liveFileStream?.Dispose();
                    _liveWaveStream = null;
                    _liveFileStream = null;
                    _liveMusicPlayer = null;
                }
                _liveSfxManager?.StopAll();
                _liveStopwatch.Reset();
            }

            public TimeSpan CurrentTime {
                get {
                    switch (_synchronizationMode) {
                        case PreviewTimingSynchronizationMode.Naive:
                            return GetCurrentTimeNaive();
                        case PreviewTimingSynchronizationMode.EstimatedCompensation:
                            return GetCurrentTimeEstimated();
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            public readonly object SfxSyncObject = new object();

            public double SfxBufferTime { get; set; }

            public SfxManager LiveSfxManager => _liveSfxManager;

            protected override void Dispose(bool disposing) {
                _liveStopwatch.Reset();
                _audioManager.Dispose();
                _liveMusicPlayer?.Stop();
                _liveSfxManager?.Dispose();
                _liveMusicPlayer?.Dispose();
                _liveTimer?.Dispose();
            }

            private void LiveTimer_Elapsed(object sender, ElapsedEventArgs e) {
                Tick?.Invoke(this, EventArgs.Empty);
            }

            private TimeSpan GetCurrentTimeNaive() {
                if (_liveWaveStream == null || (_liveMusicPlayer != null && !_liveMusicPlayer.IsPlaying)) {
                    return _liveStartTime + _liveStopwatch.Elapsed;
                } else {
                    return _liveMusicPlayer?.CurrentTime ?? TimeSpan.Zero;
                }
            }

            private TimeSpan GetCurrentTimeEstimated() {
                if (_liveWaveStream == null || (_liveMusicPlayer != null && !_liveMusicPlayer.IsPlaying)) {
                    return _liveStartTime + _liveStopwatch.Elapsed;
                }

                var stopwatchTime = _liveStopwatch.Elapsed + _liveStartTime;
                var standardTime = _liveMusicPlayer?.CurrentTime ?? TimeSpan.Zero;
                var estimatedTime = stopwatchTime - _lastStopwatchDifference;
                var newDifference = estimatedTime - standardTime;

                if (newDifference > MaxEstimationError || newDifference < -MaxEstimationError) {
                    _lastStopwatchDifference += newDifference;
                    return standardTime;
                } else {
                    return estimatedTime;
                }
            }

            private readonly Stopwatch _liveStopwatch = new Stopwatch();
            private TimeSpan _liveStartTime;

            private TimeSpan _lastStopwatchDifference;

            private readonly AudioManager _audioManager;

            [CanBeNull]
            private LiveMusicPlayer _liveMusicPlayer;
            [CanBeNull]
            private LiveMusicWaveStream _liveWaveStream;
            private Stream _liveFileStream;

            private readonly SfxManager _liveSfxManager;

            private Timer _liveTimer;

            private PreviewTimingSynchronizationMode _synchronizationMode;

            private static readonly TimeSpan MaxEstimationError = TimeSpan.FromSeconds(0.1);

        }

    }
}
