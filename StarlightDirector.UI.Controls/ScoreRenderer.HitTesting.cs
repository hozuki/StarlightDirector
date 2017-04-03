using System;
using System.Drawing;
using System.Windows.Forms;

namespace StarlightDirector.UI.Controls {
    partial class ScoreRenderer {

        public ScoreRendererHitTestResult HitTest(Point location, Control relativeTo) {
            location = relativeTo.PointToScreen(location);
            location = PointToClient(location);
            return HitTest(location);
        }

        public ScoreRendererHitTestResult HitTest(int x, int y, Control relativeTo) {
            return HitTest(new Point(x, y), relativeTo);
        }

        public ScoreRendererHitTestResult HitTest(Point location) {
            return HitTest(location.X, location.Y);
        }

        /// <summary>
        /// Performs a hit test and returns the result.
        /// </summary>
        /// <param name="x">The X coordinate of the hit test point, relative to this control.</param>
        /// <param name="y">The Y coordinate of the hit test point, relative to this control.</param>
        /// <returns>The result of this hit test.</returns>
        public ScoreRendererHitTestResult HitTest(int x, int y) {
            throw new NotImplementedException();
        }

    }
}
