using StarlightDirector.UI.Rendering.Direct2D;
using StarlightDirector.UI.Rendering.Extensions;

namespace StarlightDirector.UI.Controls.Previewing {
    partial class ScorePreviewer {

        private void RenderAvatars(D2DRenderContext context) {
            var clientSize = context.ClientSize;
            var yCenter = clientSize.Height * BaseLineYPosition;
            var radius = AvatarCircleRadius;
            var fill = _avatarFill;
            var stroke = _avatarBorderStroke;

            var xStart = clientSize.Width * AvatarCenterXEndPositions[0] - radius;
            var xEnd = clientSize.Width * AvatarCenterXEndPositions[AvatarCenterXEndPositions.Length - 1] - radius;
            context.DrawLine(stroke, xStart, yCenter, xEnd, yCenter);

            foreach (var position in AvatarCenterXEndPositions) {
                var xCenter = clientSize.Width * position;
                context.FillCircle(fill, xCenter - radius, yCenter - radius, radius);
                context.DrawCircle(stroke, xCenter, yCenter, radius);
            }
        }

    }
}
