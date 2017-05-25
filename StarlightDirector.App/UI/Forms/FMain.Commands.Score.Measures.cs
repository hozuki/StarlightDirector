using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdScoreMeasureAppend_Executed(object sender, ExecutedEventArgs e) {
            var bar = visualizer.Editor.AppendBar();
            visualizer.RecalcLayout();

            visualizer.Editor.ScrollToBar(bar);

            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreMeasureAppendMultiple_Executed(object sender, ExecutedEventArgs e) {
            var (dialogResult, numberOfMeasures) = FNewMeasures.RequestInput(this);
            if (dialogResult == DialogResult.Cancel) {
                return;
            }
            if (numberOfMeasures <= 0) {
                return;
            }

            var bars = visualizer.Editor.AppendBars(numberOfMeasures);
            visualizer.RecalcLayout();

            visualizer.Editor.ScrollToBar(bars[0]);

            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreMeasureInsert_Executed(object sender, ExecutedEventArgs e) {
            var score = visualizer.Editor.CurrentScore;
            if (score == null || !score.HasAnyBar) {
                CmdScoreMeasureAppend.Execute(sender, e);
                return;
            }

            Bar selectedBar;
            if (visualizer.Editor.HasOneSelectedBar) {
                selectedBar = visualizer.Editor.GetSelectedBar();
            } else if (visualizer.Editor.HasSelectedBars) {
                var bars = visualizer.Editor.GetSelectedBars().ToList();
                bars.Sort((b1, b2) => b1.Basic.Index.CompareTo(b2.Basic.Index));
                // Use the first selected bar.
                selectedBar = bars[0];
            } else {
                selectedBar = visualizer.Editor.GetFirstVisibleBarWithVisibleHead();
            }
            if (selectedBar == null) {
                CmdScoreMeasureAppend.Execute(sender, e);
                return;
            }

            var insertedBar = visualizer.Editor.InsertBar(selectedBar);
            visualizer.RecalcLayout();

            visualizer.Editor.ScrollToBar(insertedBar);

            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreMeasureInsertMultiple_Executed(object sender, ExecutedEventArgs e) {
            var score = visualizer.Editor.CurrentScore;
            if (score == null || !score.HasAnyBar) {
                CmdScoreMeasureAppendMultiple.Execute(sender, e);
                return;
            }

            Bar selectedBar;
            if (visualizer.Editor.HasOneSelectedBar) {
                selectedBar = visualizer.Editor.GetSelectedBar();
            } else if (visualizer.Editor.HasSelectedBars) {
                var bars = visualizer.Editor.GetSelectedBars().ToList();
                bars.Sort((b1, b2) => b1.Basic.Index.CompareTo(b2.Basic.Index));
                // Use the first selected bar.
                selectedBar = bars[0];
            } else {
                selectedBar = visualizer.Editor.GetFirstVisibleBarWithVisibleHead();
            }
            if (selectedBar == null) {
                CmdScoreMeasureAppendMultiple.Execute(sender, e);
                return;
            }

            var (dialogResult, numberOfMeasures) = FNewMeasures.RequestInput(this);
            if (dialogResult == DialogResult.Cancel) {
                return;
            }
            if (numberOfMeasures <= 0) {
                return;
            }

            var insertedBars = visualizer.Editor.InsertBars(selectedBar, numberOfMeasures);
            visualizer.RecalcLayout();

            visualizer.Editor.ScrollToBar(insertedBars[0]);

            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreMeasureDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedBars();
            visualizer.RecalcLayout();
            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();
            visualizer.Editor.Invalidate();
        }

        private readonly Command CmdScoreMeasureAppend = CommandManager.CreateCommand();
        private readonly Command CmdScoreMeasureAppendMultiple = CommandManager.CreateCommand();
        private readonly Command CmdScoreMeasureInsert = CommandManager.CreateCommand();
        private readonly Command CmdScoreMeasureInsertMultiple = CommandManager.CreateCommand();
        private readonly Command CmdScoreMeasureDelete = CommandManager.CreateCommand("Shift+Delete");

    }
}
