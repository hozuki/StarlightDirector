using MonoGame.Extended.Overlay;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.UI.Controls.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls.Previewing;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    partial class ScorePreviewerRenderer {

        private void RenderAvatars(Graphics context) {
            var clientSize = context.Bounds;
            var yCenter = clientSize.Height * Definitions.BaseLineYPosition;
            var diameter = Definitions.AvatarCircleDiameter;
            var radius = Definitions.AvatarCircleRadius;
            var fill = _avatarFill;
            var stroke = _avatarBorderStroke;

            var xStart = NotesLayerUtils.GetAvatarXPosition(clientSize, NotePosition.Min) - diameter;
            var xEnd = NotesLayerUtils.GetAvatarXPosition(clientSize, NotePosition.Max) + diameter;
            context.DrawLine(stroke, xStart, yCenter, xEnd, yCenter);

            foreach (var position in Definitions.AvatarCenterXEndPositions) {
                var xCenter = clientSize.Width * position;
                context.FillCircle(fill, xCenter, yCenter, radius);
                context.DrawCircle(stroke, xCenter, yCenter, radius);
            }
        }

    }
}
