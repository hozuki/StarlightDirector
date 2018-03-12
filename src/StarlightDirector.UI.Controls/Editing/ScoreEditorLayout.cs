using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;

namespace StarlightDirector.UI.Controls.Editing {
    internal static class ScoreEditorLayout {

        [DebuggerStepThrough]
        public static bool IsBarVisible(RectangleF barArea, float barStartY, float numberOfGrids, float unit) {
            var barHeight = numberOfGrids * unit;
            var visible = 0 <= barStartY && barStartY - barHeight <= barArea.Bottom;
            return visible;
        }

        [DebuggerStepThrough]
        public static bool IsBarHeadVisible(RectangleF barArea, float barStartY) {
            var visible = 0 <= barStartY && barStartY <= barArea.Bottom;
            return visible;
        }

        [DebuggerStepThrough]
        public static RectangleF GetGridArea(ScoreEditorConfig config, SizeF clientSize) {
            var area = new RectangleF((clientSize.Width - config.BarAreaWidth) / 2 + (config.InfoAreaWidth + config.GridNumberAreaWidth), 0, config.GridAreaWidth, clientSize.Height);
            return area;
        }

        [DebuggerStepThrough]
        public static RectangleF GetInfoArea(ScoreEditorConfig config, SizeF clientSize) {
            var area = new RectangleF((clientSize.Width - config.BarAreaWidth) / 2, 0, config.InfoAreaWidth, clientSize.Height);
            return area;
        }

        [DebuggerStepThrough]
        public static RectangleF GetBarArea(ScoreEditorConfig config, SizeF clientSize) {
            var area = new RectangleF((clientSize.Width - config.BarAreaWidth) / 2, 0, config.BarAreaWidth, clientSize.Height);
            return area;
        }

        [DebuggerStepThrough]
        public static RectangleF GetBarArea(ScoreEditorConfig config, Size clientSize) {
            var area = new RectangleF((clientSize.Width - config.BarAreaWidth) / 2, 0, config.BarAreaWidth, clientSize.Height);
            return area;
        }

        [DebuggerStepThrough]
        public static RectangleF GetSpecialNoteArea(ScoreEditorConfig config, SizeF clientSize) {
            var area = new RectangleF((clientSize.Width - config.BarAreaWidth) / 2 + (config.InfoAreaWidth + config.GridNumberAreaWidth + config.GridAreaWidth), 0, config.SpecialNotesAreaWidth, clientSize.Height);
            return area;
        }

        [DebuggerStepThrough]
        public static bool IsNoteVisible(Note note, RectangleF gridArea, float noteStartY, float unit, float radius) {
            var onStageStatus = GetNoteOnStageStatus(note, gridArea, noteStartY, unit, radius);
            return onStageStatus == OnStageStatus.OnStage;
        }

        public static OnStageStatus GetNoteOnStageStatus(Note note, RectangleF gridArea, float noteStartY, float unit, float radius) {
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
        public static float GetNotePositionX(Note note, RectangleF gridArea, int numColumns) {
            return ((int)note.Basic.FinishPosition - 1) * gridArea.Width / (numColumns - 1) + gridArea.Left;
        }

        [DebuggerStepThrough]
        public static float GetNotePositionY(Note note, float unit, float noteStartY) {
            return noteStartY - unit * note.Basic.IndexInGrid;
        }

        public static float GetNotePositionY(IEnumerable<Bar> bars, int barIndex, int indexInGrid, float unit, float scrollOffsetY) {
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

        public static readonly int[] BarZoomRatio = { 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 96 };

        public static readonly float GridNumberMargin = 22;
        public static readonly float SpaceUnitRadiusRatio = 2.2f;

    }
}
