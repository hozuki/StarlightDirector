using System;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Controls.Direct2D;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Previewing {
    public sealed partial class ScorePreviewer : Direct2DCanvas {

        public TimeSpan Now { get; private set; }

        public Score Score { get; private set; }

        public void Prepare() {
            var score = Score;
            score?.UpdateNoteHitTimings();
        }

        public void StartAnimation(TimeSpan startTime) {
            throw new NotImplementedException();
        }

        public void StopAnimation() {
            throw new NotImplementedException();
        }

        protected override void OnRender(D2DRenderContext context) {
            var score = Score;
            if (score == null) {
                return;
            }

            RenderAvatars(context);
            RenderNotes(context, score);
        }

        public static float FutureTimeWindow = 1f;
        public static readonly float PastTimeWindow = 0.2f;
        public static readonly float AvatarCircleDiameter = 30;
        public static readonly float AvatarCircleRadius = AvatarCircleDiameter / 2;
        public static readonly float[] AvatarCenterXStartPositions = { 0.272363281f, 0.381347656f, 0.5f, 0.618652344f, 0.727636719f };
        public static readonly float[] AvatarCenterXEndPositions = { 0.192382812f, 0.346191406f, 0.5f, 0.653808594f, 0.807617188f };
        public static readonly float BaseLineYPosition = 0.828125f;
        public static readonly float FutureNoteCeiling = 0.21875f;

        private static readonly float NoteShapeStrokeWidth = 1;

        private static readonly float ScaleFactor1 = 0.8f;
        private static readonly float ScaleFactor2 = 0.5f;
        private static readonly float ScaleFactor3 = (float)1 / 3f;
        private static readonly float SlideNoteStrikeHeightFactor = (float)4 / 30;

    }
}
