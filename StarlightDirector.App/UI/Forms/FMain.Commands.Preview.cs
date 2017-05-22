using System;
using System.IO;
using System.Timers;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Commanding;
using StarlightDirector.Previewing.Audio;
using StarlightDirector.UI.Controls;

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
            if (_timer == null) {
                _timer = new Timer(1);
                _timer.Elapsed += _timer_Elapsed;
            }
            var project = visualizer.Editor.Project;
            if (project.HasMusic && File.Exists(project.MusicFileName)) {
                _liveMusicPlayer = new LiveMusicPlayer();
                _liveFileStream = File.Open(project.MusicFileName, FileMode.Open, FileAccess.Read);
                _liveWaveStream = LiveMusicWaveStream.FromWaveStream(_liveFileStream);
                _liveMusicPlayer.AddInputStream(_liveWaveStream);
            }
            visualizer.Previewer.Score = visualizer.Editor.CurrentScore;
            visualizer.Previewer.Prepare();
            _lastSignalTime = DateTime.Now;
            _totalTime = TimeSpan.FromSeconds(startTime);
            if (_liveMusicPlayer != null) {
                _liveMusicPlayer.CurrentTime = _totalTime;
            }
            _timer.Start();
            _liveMusicPlayer?.Play();
            visualizer.Previewer.StartAnimation();
        }

        private void CmdPreviewFromStart_Executed(object sender, ExecutedEventArgs e) {
            if (visualizer.Previewer.IsAnimationEnabled) {
                return;
            }
            visualizer.Display = VisualizerDisplay.Previewer;
            if (_timer == null) {
                _timer = new Timer(1);
                _timer.Elapsed += _timer_Elapsed;
            }
            var project = visualizer.Editor.Project;
            if (project.HasMusic && File.Exists(project.MusicFileName)) {
                _liveMusicPlayer = new LiveMusicPlayer();
                _liveFileStream = File.Open(project.MusicFileName, FileMode.Open, FileAccess.Read);
                _liveWaveStream = LiveMusicWaveStream.FromWaveStream(_liveFileStream);
                _liveMusicPlayer.AddInputStream(_liveWaveStream);
            }
            visualizer.Previewer.Score = visualizer.Editor.CurrentScore;
            visualizer.Previewer.Prepare();
            _lastSignalTime = DateTime.Now;
            _totalTime = TimeSpan.Zero;
            _timer.Start();
            _liveMusicPlayer?.Play();
            visualizer.Previewer.StartAnimation();
        }

        private void CmdPreviewStop_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Previewer.IsAnimationEnabled) {
                return;
            }
            if (_liveMusicPlayer != null) {
                _liveMusicPlayer.Stop();
                _liveWaveStream?.Dispose();
                _liveFileStream?.Dispose();
                _liveWaveStream = null;
                _liveMusicPlayer.Dispose();
                _liveMusicPlayer = null;
            }
            _timer.Stop();
            visualizer.Previewer.StopAnimation();
            visualizer.Display = VisualizerDisplay.Editor;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e) {
            if (_liveMusicPlayer == null) {
                var signal = e.SignalTime;
                var elapsed = signal - _lastSignalTime;
                _totalTime += elapsed;
                visualizer.Previewer.Now = _totalTime;
                _lastSignalTime = signal;
            } else {
                visualizer.Previewer.Now = _liveMusicPlayer.CurrentTime;
            }
        }

        private readonly Command CmdPreviewFromThisMeasure = CommandManager.CreateCommand("F5");
        private readonly Command CmdPreviewFromStart = CommandManager.CreateCommand("Ctrl+F5");
        private readonly Command CmdPreviewStop = CommandManager.CreateCommand("F6");

        private DateTime _lastSignalTime;
        private TimeSpan _totalTime;

        private LiveMusicPlayer _liveMusicPlayer;
        private LiveMusicWaveStream _liveWaveStream;
        private Stream _liveFileStream;

        private Timer _timer;

    }
}
