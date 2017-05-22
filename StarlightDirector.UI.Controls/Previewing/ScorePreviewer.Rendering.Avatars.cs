using StarlightDirector.UI.Rendering.Direct2D;
using StarlightDirector.UI.Rendering.Extensions;

namespace StarlightDirector.UI.Controls.Previewing {
    partial class ScorePreviewer {

        private void RenderAvatars(D2DRenderContext context) {
            var clientSize = context.ClientSize;
            var yCenter = clientSize.Height * Definitions.BaseLineYPosition;
            var diameter = Definitions.AvatarCircleDiameter;
            var radius = Definitions.AvatarCircleRadius;
            var fill = _avatarFill;
            var stroke = _avatarBorderStroke;

            var xStart = clientSize.Width * Definitions.AvatarCenterXEndPositions[0] - diameter;
            var xEnd = clientSize.Width * Definitions.AvatarCenterXEndPositions[Definitions.AvatarCenterXEndPositions.Length - 1] + diameter;
            context.DrawLine(stroke, xStart, yCenter, xEnd, yCenter);

            foreach (var position in Definitions.AvatarCenterXEndPositions) {
                var xCenter = clientSize.Width * position;
                context.FillCircle(fill, xCenter, yCenter, radius);
                context.DrawCircle(stroke, xCenter, yCenter, radius);
            }
        }

    }
}
