using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using JetBrains.Annotations;
using OpenCGSS.Director.Common.ViewModels;

namespace OpenCGSS.Director.Common.Commands {
    [CommandHandler]
    public sealed class EditPasteCommandHandler : CommandHandlerBase<EditPasteCommandDefinition> {

        [ImportingConstructor]
        public EditPasteCommandHandler([NotNull] IShell shell) {
            _shell = shell;
        }

        public override Task Run(Command command) {
            var extendedDocument = _shell.ActiveItem as IBeatmapDocument;

            extendedDocument?.Paste();

            return TaskUtility.Completed;
        }

        public override void Update(Command command) {
            base.Update(command);

            var activeDocument = _shell.ActiveItem;

            if (!(activeDocument is IBeatmapDocument extendedDocument)) {
                command.Enabled = false;
            } else {
                command.Enabled = extendedDocument.CanPerformPaste;
            }
        }

        private readonly IShell _shell;

    }
}
