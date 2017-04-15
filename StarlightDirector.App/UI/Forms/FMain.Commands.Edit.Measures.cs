using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdEditMeasureAppend_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.AppendBar();
            visualizer.RecalcLayout();

            // Navigate to the start of last measure.
            var score = visualizer.Editor.CurrentScore;
            if (score != null) {
                var estY = (float)score.Bars.Take(score.Bars.Count - 1).Sum(b => b.GetNumberOfGrids());
                estY = estY * visualizer.Editor.BarLineSpaceUnit;
                estY += visualizer.ScrollBar.Minimum;
                visualizer.ScrollBar.Value = (int)estY;
            }

            visualizer.RedrawScore();
        }

        private void CmdEditMeasureAppendMany_Executed(object sender, ExecutedEventArgs e) {
            var (dialogResult, numberOfMeasures) = FAppendMeasures.RequestInput(this);
            if (dialogResult == DialogResult.Cancel) {
                return;
            }

            for (var i = 0; i < numberOfMeasures; ++i) {
                visualizer.Editor.AppendBar();
            }
            visualizer.RecalcLayout();

            // Navigate to the start of last measure.
            var score = visualizer.Editor.CurrentScore;
            if (score != null) {
                var estY = (float)score.Bars.Take(score.Bars.Count - 1).Sum(b => b.GetNumberOfGrids());
                estY = estY * visualizer.Editor.BarLineSpaceUnit;
                estY += visualizer.ScrollBar.Minimum;
                visualizer.ScrollBar.Value = (int)estY;
            }

            visualizer.RedrawScore();
        }

        private void CmdEditMeasureDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedBars();
            visualizer.RecalcLayout();
            visualizer.RedrawScore();
        }

        private readonly Command CmdEditMeasureAppend = CommandManager.CreateCommand();
        private readonly Command CmdEditMeasureAppendMany = CommandManager.CreateCommand();
        private readonly Command CmdEditMeasureDelete = CommandManager.CreateCommand("Shift+Delete");

    }
}
