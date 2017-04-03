namespace StarlightDirector.Core {
    public static class MathUtils {

        public static float Clamp(this float v, float min, float max) {
            return v < min ? min : (v > max ? max : v);
        }

        public static int Clamp(this int v, int min, int max) {
            return v < min ? min : (v > max ? max : v);
        }

    }
}
