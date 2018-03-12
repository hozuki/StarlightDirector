using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using OpenCGSS.Director.Modules.SldProject.Models;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap.Extensions;
using OpenCGSS.Director.Modules.SldProject.Services;

namespace OpenCGSS.Director.Modules.SldProject.Rendering {
    internal static class ScoreEditorLayout {

        public static readonly IReadOnlyList<int> BarZoomRatio = new[] { 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 96 };

        public const float GridNumberMargin = 22;
        public const float SpaceUnitRadiusRatio = 2.2f;

        // This is used for scaling. It can be different with signature*gps.
        public const int MaxNumberOfGrids = 96;

        [DebuggerStepThrough]
        public static bool IsBarVisible(Rectangle barArea, float barStartY, float numberOfGrids, float unit) {
            var barHeight = numberOfGrids * unit;
            var visible = 0 <= barStartY && barStartY - barHeight <= barArea.Bottom;

            return visible;
        }

        [DebuggerStepThrough]
        public static bool IsBarHeadVisible(Rectangle barArea, float barStartY) {
            var visible = 0 <= barStartY && barStartY <= barArea.Bottom;

            return visible;
        }

        [DebuggerStepThrough]
        public static Rectangle GetGridArea([NotNull] ScoreEditorConfig config, Vector2 clientSize) {
            var area = RectangleHelper.Build((clientSize.X - config.BarAreaWidth) / 2 + (config.InfoAreaWidth + config.GridNumberAreaWidth), 0, config.GridAreaWidth, clientSize.Y);

            return area;
        }

        [DebuggerStepThrough]
        public static Rectangle GetInfoArea([NotNull] ScoreEditorConfig config, Vector2 clientSize) {
            var area = RectangleHelper.Build((clientSize.X - config.BarAreaWidth) / 2, 0, config.InfoAreaWidth, clientSize.Y);

            return area;
        }

        [DebuggerStepThrough]
        public static Rectangle GetBarArea([NotNull] ScoreEditorConfig config, Vector2 clientSize) {
            var area = RectangleHelper.Build((clientSize.X - config.BarAreaWidth) / 2, 0, config.BarAreaWidth, clientSize.Y);

            return area;
        }

        [DebuggerStepThrough]
        public static Rectangle GetSpecialNoteArea([NotNull] ScoreEditorConfig config, Vector2 clientSize) {
            var area = RectangleHelper.Build((clientSize.X - config.BarAreaWidth) / 2 + (config.InfoAreaWidth + config.GridNumberAreaWidth + config.GridAreaWidth), 0, config.SpecialNotesAreaWidth, clientSize.Y);

            return area;
        }

        [DebuggerStepThrough]
        public static bool IsNoteVisible([NotNull] Note note, Rectangle gridArea, float noteStartY, float unit, float radius) {
            var onStageStatus = GetNoteOnStageStatus(note, gridArea, noteStartY, unit, radius);

            return onStageStatus == OnStageStatus.OnStage;
        }

        public static OnStageStatus GetNoteOnStageStatus([NotNull] Note note, Rectangle gridArea, float noteStartY, float unit, float radius) {
            if (note == null) {
                throw new ArgumentNullException(nameof(note));
            }

            var y = GetNotePositionY(note, unit, noteStartY);

            if (y < gridArea.Top - radius) {
                return OnStageStatus.Upcoming;
            } else if (y > gridArea.Bottom + radius) {
                return OnStageStatus.Passed;
            } else {
                return OnStageStatus.OnStage;
            }
        }

        [DebuggerStepThrough]
        public static float GetNotePositionX([NotNull] Note note, Rectangle gridArea, int numColumns) {
            return ((int)note.Basic.FinishPosition - 1) * gridArea.Width / (numColumns - 1) + gridArea.Left;
        }

        [DebuggerStepThrough]
        public static float GetNotePositionY([NotNull] Note note, float unit, float noteStartY) {
            return noteStartY - unit * note.Basic.IndexInGrid;
        }

        public static float GetNotePositionY([NotNull, ItemNotNull] IReadOnlyList<Bar> bars, int barIndex, int indexInGrid, float unit, float scrollOffsetY) {
            var noteStartY = scrollOffsetY;

            if (barIndex == 0) {
                return noteStartY - unit * indexInGrid;
            }

            var n = 0;

            foreach (var bar in bars) {
                if (n >= barIndex) {
                    break;
                }

                noteStartY -= bar.GetNumberOfGrids() * unit;
                ++n;
            }

            return noteStartY - unit * indexInGrid;
        }

    }
}
