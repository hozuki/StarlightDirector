using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Shell.Submodules {
    [Export(typeof(ITabsRecorder))]
    [Export(typeof(IModule))]
    public sealed class TabResponder : ModuleBase, ITabsRecorder {

        public override void Initialize() {
            var shell = IoC.Get<IShell>();

            // Setup document title watching.
            shell.ActiveDocumentChanging += Shell_ActiveDocumentChanging;
            shell.ActiveDocumentChanged += Shell_ActiveDocumentChanged;
            Shell_ActiveDocumentChanged(this, EventArgs.Empty);

            // Set main window icon.
            var mainWindow = IoC.Get<IMainWindow>();

            mainWindow.Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/SD-Icon.ico"));

            // Show main toolbar
            shell.ToolBars.Visible = true;
        }

        public IDocument LastDocument => _lastDocument;

        private void Shell_ActiveDocumentChanging(object sender, EventArgs e) {
            var shell = IoC.Get<IShell>();

            var document = shell.ActiveItem;

            if (document != null) {
                document.PropertyChanged -= Document_PropertyChanged_FilterDisplayName;
            }

            _lastDocument = document;
        }

        private void Shell_ActiveDocumentChanged(object sender, EventArgs e) {
            var mainWindow = IoC.Get<IMainWindow>();
            var shell = IoC.Get<IShell>();

            var document = shell.ActiveItem;

            // This behavior is buggy in Gemini.
            // What happens in Gemini (in March 2018) when a document is created is:
            // 1. Active document changed;
            // 2. Document's editor provider's New() is called (e.g. SldprojEditorProvider);
            // 3. Document's DisplayName is set to "Untitled {N}{EXTENSION}".
            // 4. Document's view model's OnNew() is called.
            // So if we read document.DisplayName here, we only get its default display name (that is, the full name of its type).
            // To get its real name, we have to set up a property changed listener to observe its DisplayName property change.
            // No need to say, that this handler must be managed by ourselves.
            // Since we don't know the document's exact static type, we cannot use WeakEventManager<T,U>.
            // Old school event management. Oh yeah baby. :/
            if (document == null) {
                mainWindow.Title = GetFormattedMainWindowTitle(null);
            } else {
                if (document.DisplayName != null) {
                    mainWindow.Title = GetFormattedMainWindowTitle(document.DisplayName);
                } else {
                    document.PropertyChanged += Document_PropertyChanged_FilterDisplayName;
                }
            }
        }

        private void Document_PropertyChanged_FilterDisplayName(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName != nameof(IDocument.DisplayName)) {
                return;
            }

            var mainWindow = IoC.Get<IMainWindow>();
            var document = (IDocument)sender;

            mainWindow.Title = GetFormattedMainWindowTitle(document.DisplayName);
        }

        private static string GetFormattedMainWindowTitle([CanBeNull] string documentDisplayName) {
            if (documentDisplayName == null) {
                return BaseTitle;
            } else {
                return documentDisplayName + " - " + BaseTitle;
            }
        }

        private const string BaseTitle = "Starlight Director";

        private IDocument _lastDocument;

    }
}
