using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Common.Commands {
    [CommandDefinition]
    public sealed class EditViewPropertiesCommandDefinition : CommandDefinition {

        public override string Name => "Edit.ViewProperties";

        public override string Text => "View P_roperties";

        public override string ToolTip => "View document properties.";

    }
}
