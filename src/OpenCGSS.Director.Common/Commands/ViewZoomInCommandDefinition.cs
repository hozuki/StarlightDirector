using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Common.Commands {
    [CommandDefinition]
    public sealed class ViewZoomInCommandDefinition : CommandDefinition {

        public override string Name => "View.ZoomIn";

        public override string Text => "Zoom _In";

        public override string ToolTip => "Zoom in.";

    }
}
