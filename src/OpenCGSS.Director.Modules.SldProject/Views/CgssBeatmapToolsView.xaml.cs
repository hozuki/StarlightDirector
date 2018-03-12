using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Gemini.Framework.Services;
using Gemini.Framework.ShaderEffects;
using JetBrains.Annotations;
using OpenCGSS.Director.Common;
using OpenCGSS.Director.Modules.SldProject.Models;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap;
using OpenCGSS.Director.Modules.SldProject.ViewModels;

namespace OpenCGSS.Director.Modules.SldProject.Views {
    /// <summary>
    /// CgssBeatmapEditorView.xaml 的交互逻辑
    /// </summary>
    public partial class CgssBeatmapToolsView : UserControl {

        public CgssBeatmapToolsView() {
            _shell = IoC.Get<IShell>();

            InitializeComponent();

            CommandHelper.InitializeCommandBindings(this);
        }

        public void UpdateWindowState() {
            UpdateWindowState(_shell.ActiveItem as SldProjViewModel);
        }

        public void UpdateWindowState([CanBeNull] SldProjViewModel beatmapDocument) {
            if (beatmapDocument == null) {
                IsEnabled = false;
                return;
            }

            IsEnabled = true;

            var view = (SldProjView)beatmapDocument.GetView();

            // Now the data...

            // Difficulty
            DifficultyComboBox.SelectedIndex = (int)beatmapDocument.SelectedDifficulty - (int)Difficulty.Debut;

            // Edit mode
            var currentEditMode = beatmapDocument.EditMode;

            switch (currentEditMode) {
                case ScoreEditMode.Select:
                    BtnEditModeSelect.Effect = _editModeButtonGrayscaleEffect;
                    BtnEditModeTap.Effect = null;
                    BtnEditModeHoldFlick.Effect = null;
                    BtnEditModeSlide.Effect = null;
                    SelectedEditModeImage.Source = ((Image)BtnEditModeSelect.Icon).Source;
                    break;
                case ScoreEditMode.Tap:
                    BtnEditModeSelect.Effect = null;
                    BtnEditModeTap.Effect = _editModeButtonGrayscaleEffect;
                    BtnEditModeHoldFlick.Effect = null;
                    BtnEditModeSlide.Effect = null;
                    SelectedEditModeImage.Source = ((Image)BtnEditModeTap.Icon).Source;
                    break;
                case ScoreEditMode.HoldFlick:
                    BtnEditModeSelect.Effect = null;
                    BtnEditModeTap.Effect = null;
                    BtnEditModeHoldFlick.Effect = _editModeButtonGrayscaleEffect;
                    BtnEditModeSlide.Effect = null;
                    SelectedEditModeImage.Source = ((Image)BtnEditModeHoldFlick.Icon).Source;
                    break;
                case ScoreEditMode.Slide:
                    BtnEditModeSelect.Effect = null;
                    BtnEditModeTap.Effect = null;
                    BtnEditModeHoldFlick.Effect = null;
                    BtnEditModeSlide.Effect = _editModeButtonGrayscaleEffect;
                    SelectedEditModeImage.Source = ((Image)BtnEditModeSlide.Icon).Source;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ChkShowIndicators.IsChecked = view.ScoreEditorRenderer.Look.IndicatorsVisible;

            CommandManager.InvalidateRequerySuggested();
        }

        private readonly System.Windows.Media.Effects.Effect _editModeButtonGrayscaleEffect = new GrayscaleEffect();

        private readonly IShell _shell;

    }
}
