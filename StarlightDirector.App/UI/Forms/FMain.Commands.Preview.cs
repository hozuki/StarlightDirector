using System;
using System.Timers;
using StarlightDirector.Commanding;
using StarlightDirector.UI.Controls;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdPreviewFromThisMeasure_Executed(object sender, ExecutedEventArgs e) {
            // TODO
        }

        private void CmdPreviewFromThisMeasure_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = false;
        }

        private void CmdPreviewFromStart_Executed(object sender, ExecutedEventArgs e) {
            // TODO
            switch (visualizer.Display) {
                case VisualizerDisplay.Editor:
                    visualizer.Display = VisualizerDisplay.Previewer;
                    break;
                case VisualizerDisplay.Previewer:
                    visualizer.Display = VisualizerDisplay.Editor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CmdPreviewFromStart_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = true;
        }

        private void CmdPreviewStop_Executed(object sender, ExecutedEventArgs e) {
            // TODO
            if (visualizer.Previewer.IsAnimationEnabled) {
                _timer.Stop();
                visualizer.Previewer.StopAnimation();
            } else {
                if (_timer == null) {
                    _timer = new Timer(5);
                    _timer.Elapsed += _timer_Elapsed;
                }
                visualizer.Previewer.Score = visualizer.Editor.CurrentScore;
                visualizer.Previewer.Prepare();
                _lastSignalTime = DateTime.Now;
                _totalTime = TimeSpan.Zero;
                _timer.Start();
                visualizer.Previewer.StartAnimation();
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e) {
            var signal = e.SignalTime;
            var elapsed = signal - _lastSignalTime;
            _totalTime += elapsed;
            visualizer.Previewer.Now = _totalTime;
            _lastSignalTime = signal;
        }

        private void CmdPreviewStop_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = true;
        }

        private readonly Command CmdPreviewFromThisMeasure = CommandManager.CreateCommand("F5");
        private readonly Command CmdPreviewFromStart = CommandManager.CreateCommand("Ctrl+F5");
        private readonly Command CmdPreviewStop = CommandManager.CreateCommand("F6");

        private DateTime _lastSignalTime;
        private TimeSpan _totalTime;

        private Timer _timer;

    }
}
