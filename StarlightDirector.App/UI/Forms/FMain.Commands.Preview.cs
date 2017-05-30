using System;
using System.IO;
using System.Windows.Forms;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Commanding;
using StarlightDirector.Core;
using StarlightDirector.Previewing.Audio;
using StarlightDirector.UI.Controls;
using Timer = System.Timers.Timer;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdPreviewFromThisMeasure_Executed(object sender, ExecutedEventArgs e) {
            if (visualizer.Previewer.IsAnimationEnabled) {
                return;
            }

            double startTime = 0;
            var bar = visualizer.Editor.GetFirstVisibleBar();
            if (bar != null) {
                bar.UpdateStartTime();
                startTime = bar.Temporary.StartTime.TotalSeconds - 1;
                if (startTime < 0) {
                    startTime = 0;
                }
            }

            visualizer.Display = VisualizerDisplay.Previewer;
            if (_liveTimer == null) {
                _liveTimer = new Timer(1);
                _liveTimer.Elapsed += LiveTimer_Elapsed;
            }
            var project = visualizer.Editor.Project;
            if (project.HasMusic) {
                string errorMessageTemplate = null;
                if (File.Exists(project.MusicFileName)) {
                    try {
                        _liveFileStream = File.Open(project.MusicFileName, FileMode.Open, FileAccess.Read);
                        _liveWaveStream = LiveMusicWaveStream.FromWaveStream(_liveFileStream);
                        _liveMusicPlayer.AddMusicInputStream(_liveWaveStream);
                    } catch (IOException) {
                        errorMessageTemplate = LanguageManager.TryGetString("messages.fmain.live_music_open_error_template") ?? "Cannot open '{0}'. Live music disabled.";
                    }
                } else {
                    errorMessageTemplate = LanguageManager.TryGetString("messages.fmain.live_music_not_found_template") ?? "File '{0}' does not exist. Live music disabled.";
                }
                if (errorMessageTemplate != null) {
                    var errorMessage = string.Format(errorMessageTemplate, project.MusicFileName);
                    MessageBox.Show(this, errorMessage, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _liveMusicPlayer.AddMusicInputStream(NullWaveStream.Instance);
                }
            } else {
                _liveMusicPlayer.AddMusicInputStream(NullWaveStream.Instance);
            }
            visualizer.Previewer.Score = visualizer.Editor.CurrentScore;
            visualizer.Previewer.Prepare();
            _lastSignalTime = DateTime.Now;
            _totalLiveTime = TimeSpan.FromSeconds(startTime);
            _liveMusicPlayer.CurrentTime = _totalLiveTime;
            _sfxBufferTime = 0;
            _liveTimer.Start();
            _liveMusicPlayer.Play();
            visualizer.Previewer.StartAnimation();
        }

        private void CmdPreviewFromStart_Executed(object sender, ExecutedEventArgs e) {
            if (visualizer.Previewer.IsAnimationEnabled) {
                return;
            }
            visualizer.Display = VisualizerDisplay.Previewer;
            if (_liveTimer == null) {
                _liveTimer = new Timer(1);
                _liveTimer.Elapsed += LiveTimer_Elapsed;
            }
            var project = visualizer.Editor.Project;
            if (project.HasMusic) {
                string errorMessageTemplate = null;
                if (File.Exists(project.MusicFileName)) {
                    try {
                        _liveFileStream = File.Open(project.MusicFileName, FileMode.Open, FileAccess.Read);
                        _liveWaveStream = LiveMusicWaveStream.FromWaveStream(_liveFileStream);
                        _liveMusicPlayer.AddMusicInputStream(_liveWaveStream);
                    } catch (IOException) {
                        errorMessageTemplate = LanguageManager.TryGetString("messages.fmain.live_music_open_error_template") ?? "Cannot open '{0}'. Live music disabled.";
                    }
                } else {
                    errorMessageTemplate = LanguageManager.TryGetString("messages.fmain.live_music_not_found_template") ?? "File '{0}' does not exist. Live music disabled.";
                }
                if (errorMessageTemplate != null) {
                    var errorMessage = string.Format(errorMessageTemplate, project.MusicFileName);
                    MessageBox.Show(this, errorMessage, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _liveMusicPlayer.AddMusicInputStream(NullWaveStream.Instance);
                }
            } else {
                _liveMusicPlayer.AddMusicInputStream(NullWaveStream.Instance);
            }
            visualizer.Previewer.Score = visualizer.Editor.CurrentScore;
            visualizer.Previewer.Prepare();
            _lastSignalTime = DateTime.Now;
            _totalLiveTime = TimeSpan.Zero;
            _liveMusicPlayer.CurrentTime = TimeSpan.Zero;
            _sfxBufferTime = 0;
            _liveTimer.Start();
            _liveMusicPlayer.Play();
            visualizer.Previewer.StartAnimation();
        }

        private void CmdPreviewStop_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Previewer.IsAnimationEnabled) {
                return;
            }
            _liveTimer.Stop();
            _liveMusicPlayer.Stop();
            if (_liveWaveStream != null) {
                _liveMusicPlayer.RemoveMusicInputStream();
                _liveWaveStream.Dispose();
                _liveFileStream.Dispose();
                _liveWaveStream = null;
                _liveFileStream = null;
            } else if (_liveMusicPlayer.HasMusicInputStream) {
                _liveMusicPlayer.RemoveMusicInputStream();
            }
            _liveSfxManager?.StopAll();
            visualizer.Previewer.StopAnimation();
            visualizer.Display = VisualizerDisplay.Editor;
        }

        private readonly Command CmdPreviewFromThisMeasure = CommandManager.CreateCommand("F5");
        private readonly Command CmdPreviewFromStart = CommandManager.CreateCommand("Ctrl+F5");
        private readonly Command CmdPreviewStop = CommandManager.CreateCommand("F6");

        private DateTime _lastSignalTime;
        private TimeSpan _totalLiveTime;

        private LiveMusicPlayer _liveMusicPlayer;
        private LiveMusicWaveStream _liveWaveStream;
        private Stream _liveFileStream;

        private SfxManager _liveSfxManager;

        private Timer _liveTimer;

    }
}
