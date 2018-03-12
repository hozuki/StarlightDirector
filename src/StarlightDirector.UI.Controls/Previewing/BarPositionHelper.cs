using System;
using System.Collections.Generic;
using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Controls.Editing;

namespace StarlightDirector.UI.Controls.Previewing {
    internal static class BarPositionHelper {

        public static float GetScrollOffsetY(Score score, IReadOnlyList<IReadOnlyList<TimeSpan>> scoreTimingInfo, TimeSpan now, ScoreEditorLook look, Size clientSize, float fullHeight) {
            if (!score.HasAnyBar) {
                return 0;
            }

            var middleY = clientSize.Height / 2;
            var unit = look.BarLineSpaceUnit;

            // If we haven't reached the first note...
            var firstBarTimingInfo = scoreTimingInfo[0];
            if (now < firstBarTimingInfo[0]) {
                var firstDeltaTime = firstBarTimingInfo[1] - firstBarTimingInfo[0];
                var expectedUnits = unit * (float)(firstBarTimingInfo[0].TotalSeconds / firstDeltaTime.TotalSeconds);
                var firstRatio = (float)((now - firstBarTimingInfo[0]).TotalSeconds / scoreTimingInfo[0][0].TotalSeconds);
                return middleY + expectedUnits * firstRatio;
            }

            // Or the song has ended...
            var lastBarInfo = scoreTimingInfo[scoreTimingInfo.Count - 1];
            if (now > lastBarInfo[lastBarInfo.Count - 1]) {
                var lastDeltaTime = lastBarInfo[lastBarInfo.Count - 1] - lastBarInfo[lastBarInfo.Count - 2];
                var expectedUnits = unit * (float)((now - lastBarInfo[lastBarInfo.Count - 1]).TotalSeconds / lastDeltaTime.TotalSeconds);
                return middleY + fullHeight + expectedUnits;
            }

            // Otherwise, scan through the bars and find the first match.
            var currentBar = score.Bars[0];
            for (var i = 1; i < score.Bars.Count; ++i) {
                if (scoreTimingInfo[i][0] >= now) {
                    break;
                }
                currentBar = score.Bars[i];
            }

            // Find the grid.
            var currentBarIndex = currentBar.Basic.Index;
            var currentBarTimingInfo = scoreTimingInfo[currentBarIndex];
            var prevTimingIndex = 0;
            for (var i = 1; i < currentBarTimingInfo.Count; ++i) {
                if (currentBarTimingInfo[i] > now) {
                    break;
                }
                prevTimingIndex = i;
            }

            TimeSpan nextTiming;
            var prevTiming = currentBarTimingInfo[prevTimingIndex];
            if (prevTimingIndex == currentBarTimingInfo.Count - 1) {
                // The "last line of last bar" occasion is already filtered out,
                // so here we handle a simpler case.
                nextTiming = scoreTimingInfo[currentBarIndex + 1][0];
            } else {
                nextTiming = currentBarTimingInfo[prevTimingIndex + 1];
            }

            var ratio = (float)((now - prevTiming).TotalSeconds / (nextTiming - prevTiming).TotalSeconds);

            float offset = middleY;
            for (var i = 0; i < currentBarIndex; ++i) {
                offset += unit * score.Bars[i].GetNumberOfGrids();
            }
            offset += unit * prevTimingIndex + unit * ratio;

            return offset;
        }

    }
}
