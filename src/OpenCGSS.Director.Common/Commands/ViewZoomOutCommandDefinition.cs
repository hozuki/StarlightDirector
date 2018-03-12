using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Common.Commands {
    [CommandDefinition]
    public sealed class ViewZoomOutCommandDefinition : CommandDefinition {

        public override string Name => "View.ZoomOut";

        public override string Text => "Zoom _Out";

        public override string ToolTip => "Zoom out.";

    }
}
