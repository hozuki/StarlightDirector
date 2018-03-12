using System.Threading.Tasks;
using Caliburn.Micro;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using OpenCGSS.Director.Shell.Submodules;

namespace OpenCGSS.Director.Shell.Commands {
    [CommandHandler]
    public sealed class SwitchBetweenTabsCommandHandler : CommandHandlerBase<SwitchBetweenTabsCommandDefinition> {

        public override Task Run(Command command) {
            var recorder = IoC.Get<ITabsRecorder>();

            if (recorder.LastDocument != null) {
                recorder.LastDocument.IsSelected = true;
            }

            return TaskUtility.Completed;
        }

        public override void Update(Command command) {
            base.Update(command);

            var shell = IoC.Get<IShell>();

            command.Enabled = shell.Documents.Count > 1;
        }

    }
}
