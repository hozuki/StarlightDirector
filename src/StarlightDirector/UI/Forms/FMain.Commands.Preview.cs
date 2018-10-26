using System.Windows.Forms;
using System.Windows.Forms.Input;
using OpenCGSS.StarlightDirector.DirectorApplication;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain {

        private void CmdPreviewFromThisMeasure_Executed(object sender, ExecutedEventArgs e) {
            if (visualizer.Display == VisualizerDisplay.Previewer) {
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

            var project = visualizer.Editor.Project;
            if (project.Project.HasMusic) {
                _liveControl.Load(project.Project.MusicFileName, out var errorMessageTemplate);

                if (errorMessageTemplate != null) {
                    var errorMessage = string.Format(errorMessageTemplate, project.Project.MusicFileName);
                    MessageBox.Show(this, errorMessage, AssemblyHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            visualizer.Previewer.Score = visualizer.Editor.CurrentScore;
            visualizer.Previewer.Prepare();
            visualizer.Previewer.RenderMode = DirectorSettingsManager.CurrentSettings.PreviewRenderMode;
            visualizer.Previewer.SimulateEditor(visualizer.Editor);

            _liveControl.Tick += LiveControl_Tick;

            _liveControl.StartFromTime(startTime);

            UpdateUIIndications();
        }

        private void CmdPreviewFromStart_Executed(object sender, ExecutedEventArgs e) {
            if (visualizer.Display == VisualizerDisplay.Previewer) {
                return;
            }

            visualizer.Display = VisualizerDisplay.Previewer;

            var project = visualizer.Editor.Project;

            if (project.Project.HasMusic) {
                _liveControl.Load(project.Project.MusicFileName, out var errorMessageTemplate);

                if (errorMessageTemplate != null) {
                    var errorMessage = string.Format(errorMessageTemplate, project.Project.MusicFileName);
                    MessageBox.Show(this, errorMessage, AssemblyHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            visualizer.Previewer.Score = visualizer.Editor.CurrentScore;
            visualizer.Previewer.Prepare();
            visualizer.Previewer.RenderMode = DirectorSettingsManager.CurrentSettings.PreviewRenderMode;
            visualizer.Previewer.SimulateEditor(visualizer.Editor);

            _liveControl.Tick += LiveControl_Tick;

            _liveControl.Start();

            UpdateUIIndications();
        }

        private void CmdPreviewStop_Executed(object sender, ExecutedEventArgs e) {
            if (visualizer.Display == VisualizerDisplay.Editor) {
                return;
            }

            _liveControl.Stop();

            visualizer.Display = VisualizerDisplay.Editor;

            UpdateUIIndications();
        }

        internal readonly CommandBinding CmdPreviewFromThisMeasure = CommandHelper.CreateUIBinding("F5");
        internal readonly CommandBinding CmdPreviewFromStart = CommandHelper.CreateUIBinding("Ctrl+F5");
        internal readonly CommandBinding CmdPreviewStop = CommandHelper.CreateUIBinding("F6");

        private LiveControl _liveControl = new LiveControl();

    }
}
