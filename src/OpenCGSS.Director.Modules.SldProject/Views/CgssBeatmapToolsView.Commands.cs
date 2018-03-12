using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OpenCGSS.Director.Common;
using OpenCGSS.Director.Modules.SldProject.Models;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap;
using OpenCGSS.Director.Modules.SldProject.ViewModels;

namespace OpenCGSS.Director.Modules.SldProject.Views {
    partial class CgssBeatmapToolsView {

        public static readonly ICommand CmdChangeEditModeSelect = CommandHelper.RegisterCommand(nameof(CmdChangeEditModeSelect), typeof(CgssBeatmapToolsView));
        public static readonly ICommand CmdChangeEditModeTap = CommandHelper.RegisterCommand(nameof(CmdChangeEditModeTap), typeof(CgssBeatmapToolsView));
        public static readonly ICommand CmdChangeEditModeHoldFlick = CommandHelper.RegisterCommand(nameof(CmdChangeEditModeHoldFlick), typeof(CgssBeatmapToolsView));
        public static readonly ICommand CmdChangeEditModeSlide = CommandHelper.RegisterCommand(nameof(CmdChangeEditModeSlide), typeof(CgssBeatmapToolsView));

        private void CmdChangeEditModeSelect_Executed(object sender, ExecutedRoutedEventArgs e) {
            ChangeEditMode(ScoreEditMode.Select);
        }

        private void CmdChangeEditModeSelect_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (!(_shell.ActiveItem is SldProjViewModel beatmapDocument)) {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = beatmapDocument.EditMode != ScoreEditMode.Select;
        }

        private void CmdChangeEditModeTap_Executed(object sender, ExecutedRoutedEventArgs e) {
            ChangeEditMode(ScoreEditMode.Tap);
        }

        private void CmdChangeEditModeTap_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (!(_shell.ActiveItem is SldProjViewModel beatmapDocument)) {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = beatmapDocument.EditMode != ScoreEditMode.Tap;
        }

        private void CmdChangeEditModeHoldFlick_Executed(object sender, ExecutedRoutedEventArgs e) {
            ChangeEditMode(ScoreEditMode.HoldFlick);
        }

        private void CmdChangeEditModeHoldFlick_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (!(_shell.ActiveItem is SldProjViewModel beatmapDocument)) {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = beatmapDocument.EditMode != ScoreEditMode.HoldFlick;
        }

        private void CmdChangeEditModeSlide_Executed(object sender, ExecutedRoutedEventArgs e) {
            ChangeEditMode(ScoreEditMode.Slide);
        }

        private void CmdChangeEditModeSlide_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (!(_shell.ActiveItem is SldProjViewModel beatmapDocument)) {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = beatmapDocument.EditMode != ScoreEditMode.Slide;
        }

        private void ChangeEditMode(ScoreEditMode editMode) {
            if (!(_shell.ActiveItem is SldProjViewModel beatmapDocument)) {
                return;
            }

            var newEditMode = editMode;

            beatmapDocument.EditMode = newEditMode;

            BtnEditMode.IsOpen = false;

            UpdateWindowState();
        }

        private void DifficultyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (!(_shell.ActiveItem is SldProjViewModel beatmapDocument)) {
                return;
            }

            if (DifficultyComboBox.SelectedIndex != (int)beatmapDocument.SelectedDifficulty - (int)Difficulty.Debut) {
                var newDifficulty = (Difficulty)(DifficultyComboBox.SelectedIndex + (int)Difficulty.Debut);

                beatmapDocument.SelectedDifficulty = newDifficulty;

                beatmapDocument.UpdateAllBarStartTimes();
                beatmapDocument.InvalidateView();
            }
        }

        private void ChkShowIndicators_OnChecked(object sender, RoutedEventArgs e) {
            if (!(_shell.ActiveItem is SldProjViewModel beatmapDocument)) {
                return;
            }

            var view = (SldProjView)beatmapDocument.GetView();

            view.ScoreEditorRenderer.Look.IndicatorsVisible = true;

            beatmapDocument.InvalidateView();
        }

        private void ChkShowIndicators_OnUnchecked(object sender, RoutedEventArgs e) {
            if (!(_shell.ActiveItem is SldProjViewModel beatmapDocument)) {
                return;
            }

            var view = (SldProjView)beatmapDocument.GetView();

            view.ScoreEditorRenderer.Look.IndicatorsVisible = false;

            beatmapDocument.InvalidateView();
        }

    }
}
