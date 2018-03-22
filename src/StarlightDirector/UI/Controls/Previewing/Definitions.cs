using System;

namespace OpenCGSS.StarlightDirector.UI.Controls.Previewing {
    internal static class Definitions {

        public static readonly float AvatarFrameImageCellWidth = 1008;
        public static readonly float AvatarFrameImageCellHeight = 142;

        public static float FutureTimeWindow = 1f;
        public static readonly float PastTimeWindow = 0.2f;
        public static readonly float AvatarCircleDiameter = 50;
        public static readonly float AvatarCircleRadius = AvatarCircleDiameter / 2;
        public static readonly float[] AvatarCenterXStartPositions = { 0.3f, 0.4f, 0.5f, 0.6f, 0.7f };
        public static readonly float[] AvatarCenterXEndPositions = { 0.18f, 0.34f, 0.5f, 0.66f, 0.82f };
        public static readonly float BaseLineYPosition = 0.828125f;
        // Then we know the bottom is <BaseLineYPosition + (PastWindow / FutureWindow) * (BaseLineYPosition - Ceiling))>.
        public static readonly float FutureNoteCeiling = 0.21875f;

        public static readonly float NoteShapeStrokeWidth = 1;

        public static readonly float ScaleFactor0 = 1.15f;
        public static readonly float ScaleFactor1 = 0.8f;
        public static readonly float ScaleFactor2 = 0.5f;
        public static readonly float ScaleFactor3 = 1 / 3f;
        public static readonly float SlideNoteStrikeHeightFactor = (float)4 / 30;

        public static readonly float Sqrt3 = (float)Math.Sqrt(3);

    }
}
