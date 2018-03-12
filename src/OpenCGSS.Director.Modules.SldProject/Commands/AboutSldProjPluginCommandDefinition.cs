using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Modules.SldProject.Commands {
    [CommandDefinition]
    public sealed class AboutSldProjPluginCommandDefinition : CommandDefinition {

        public override string Name => "Help.AboutPlugins.SldProj";

        public override string Text => "CGSS Beatmap";

        public override string ToolTip => "Show information about CGSS Beatmap plugin.";

    }
}
