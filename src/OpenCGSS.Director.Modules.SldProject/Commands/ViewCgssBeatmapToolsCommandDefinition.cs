using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Modules.SldProject.Commands {
    [CommandDefinition]
    public sealed class ViewCgssBeatmapToolsCommandDefinition : CommandDefinition {

        public override string Name => "View.CgssBeatmapTools";

        public override string Text => "CGSS Beatmap Tools";

        public override string ToolTip => "Toggle CGSS beatmap tool window.";

    }
}
