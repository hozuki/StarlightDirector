using System.Threading.Tasks;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using OpenCGSS.Director.Modules.SldProject.Views;

namespace OpenCGSS.Director.Modules.SldProject.Commands {
    [CommandHandler]
    public sealed class AboutSldProjPluginCommandHandler : CommandHandlerBase<AboutSldProjPluginCommandDefinition> {

        public override Task Run(Command command) {
            var view = new AboutSldProjPluginView();

            view.ShowDialog();

            return TaskUtility.Completed;
        }

    }
}
