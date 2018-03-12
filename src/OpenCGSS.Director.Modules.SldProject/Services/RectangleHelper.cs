using Microsoft.Xna.Framework;

namespace OpenCGSS.Director.Modules.SldProject.Services {
    internal static class RectangleHelper {

        public static Rectangle Build(float x, float y, float width, float height) {
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

    }
}
