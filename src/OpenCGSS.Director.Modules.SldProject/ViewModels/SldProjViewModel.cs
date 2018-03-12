using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using JetBrains.Annotations;
using OpenCGSS.Director.Common;
using OpenCGSS.Director.Common.ViewModels;
using OpenCGSS.Director.Modules.SldProject.IO;
using OpenCGSS.Director.Modules.SldProject.Models;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap.Extensions;
using OpenCGSS.Director.Modules.SldProject.Views;

namespace OpenCGSS.Director.Modules.SldProject.ViewModels {
    public sealed class SldProjViewModel : PersistedDocument, IBeatmapDocument {

        public override object GetView(object context = null) {
            return _view ?? (_view = new SldProjView());
        }

        public void Cut() {
            throw new NotImplementedException();
        }

        public void Copy() {
            throw new NotImplementedException();
        }

        public void Paste() {
            throw new NotImplementedException();
        }

        public void SelectAll() {
            throw new NotImplementedException();
        }

        public void ViewProperties() {
            throw new NotImplementedException();
        }

        public void ZoomIn() {
            throw new NotImplementedException();
        }

        public void ZoomOut() {
            throw new NotImplementedException();
        }

        public bool CanPerformCut { get; }

        public bool CanPerformCopy { get; }

        public bool CanPerformPaste { get; }

        public bool CanPerformSelectAll { get; }

        public bool CanPerformViewProperties { get; }

        public bool CanPerformZoomIn { get; }

        public bool CanPerformZoomOut { get; }

        /// <summary>
        /// Invalidates the view, forcing it to repaint and recalculate the total length of bars (which reflects on the scrollbar).
        /// </summary>
        public void InvalidateView(InvalidateSldProjViewActions actions = InvalidateSldProjViewActions.All) {
            if (_view == null) {
                return;
            }

            if ((actions & InvalidateSldProjViewActions.UpdateVerticalLength) != 0) {
                _view.UpdateVerticalLength();
            }

            if ((actions & InvalidateSldProjViewActions.Redraw) != 0) {
                _view.Redraw();
            }
        }

        public void UpdateAllBarStartTimes() {
            var score = Project?.GetScore(SelectedDifficulty);

            score?.UpdateAllStartTimes();
        }

        public Difficulty SelectedDifficulty {
            get => _selectedDifficulty;
            set {
                _selectedDifficulty = value;
                NotifyOfPropertyChange(nameof(SelectedDifficulty));
            }
        }

        public ScoreEditMode EditMode {
            get => _editMode;
            set {
                _editMode = value;
                NotifyOfPropertyChange(nameof(EditMode));
            }
        }

        [CanBeNull]
        public Project Project { get; private set; }

        [CanBeNull]
        public Score GetCurrentScore() {
            return Project?.GetScore(SelectedDifficulty);
        }

        protected override Task DoNew() {
            Project = new Project();

            if (_view != null) {
                _view.SldProject = this;
                _view.ScrollOffsetY = 0;
            }

            return TaskUtility.Completed;
        }

        protected override Task DoLoad(string filePath) {
            var shell = IoC.Get<IShell>();
            var hasAnotherInstance = false;

            foreach (var document in shell.Documents) {
                if (document is SldProjViewModel sldProj) {
                    var thatFilePath = sldProj.FilePath;

                    if (document != this && thatFilePath == filePath) {
                        hasAnotherInstance = true;
                        break;
                    }
                }
            }

            if (hasAnotherInstance) {
                TryClose(false);

                return TaskUtility.Completed;
            }

            ProjectReader reader = null;
            var projectVersion = ProjectReader.CheckFormatVersion(filePath);

            if (projectVersion == 0) {
                const string projectVersionErrorMessage = "Unable to open this project. Maybe it is corrupted or it is a project of a no longer supported version. If you created this project file using a previous version of Starlight Director, you can try to open it in v0.7.5 and save it again.";
                MessageBox.Show(projectVersionErrorMessage, ApplicationHelper.GetTitle(), MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                switch (projectVersion) {
                    case ProjectVersion.V0_2:
                        reader = new SldprojV2Reader();
                        break;
                    case ProjectVersion.V0_3:
                    case ProjectVersion.V0_3_1:
                        reader = new SldprojV3Reader();
                        break;
                    case ProjectVersion.V0_4:
                        reader = new SldprojV4Reader();
                        break;
                    default:
                        MessageBox.Show("You should not have seen this message.", ApplicationHelper.GetTitle(), MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }

            if (reader == null) {
                TryClose(false);

                return TaskUtility.Completed;
            }

            var project = reader.ReadProject(filePath, null);
            Project = project;

            project.GetScore(SelectedDifficulty)?.UpdateAllStartTimes();

            if (_view != null) {
                _view.SldProject = this;
                _view.ScrollOffsetY = 0;
            }

            _view?.UpdateVerticalLength();

            return TaskUtility.Completed;
        }

        protected override Task DoSave(string filePath) {
            var writer = ProjectWriter.CreateLatest();
            var project = Project;

            Debug.Assert(project != null, nameof(project) + " != null");

            writer.WriteProject(project, filePath);

            return TaskUtility.Completed;
        }

        protected override void OnViewLoaded(object view) {
            _view = (SldProjView)view;
        }

        private Difficulty _selectedDifficulty = Difficulty.Master;
        private ScoreEditMode _editMode = ScoreEditMode.Select;

        private SldProjView _view;

    }
}
