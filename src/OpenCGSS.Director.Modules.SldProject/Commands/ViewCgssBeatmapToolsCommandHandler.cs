using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using JetBrains.Annotations;
using OpenCGSS.Director.Modules.SldProject.ViewModels;

namespace OpenCGSS.Director.Modules.SldProject.Commands {
    [CommandHandler]
    public sealed class ViewCgssBeatmapToolsCommandHandler : CommandHandlerBase<ViewCgssBeatmapToolsCommandDefinition> {

        [ImportingConstructor]
        public ViewCgssBeatmapToolsCommandHandler([NotNull] IShell shell) {
            _shell = shell;
        }

        public override Task Run(Command command) {
            _shell.ShowTool<ICgssBeatmapTools>();

            return TaskUtility.Completed;
        }

        private readonly IShell _shell;

    }
}
