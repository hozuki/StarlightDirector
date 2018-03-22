using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;
using OpenCGSS.StarlightDirector.Models.Beatmap;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    internal abstract class RibbonMesh {

        public RibbonMesh(Graphics context, Note startNote, Note endNote, double now) {
            RenderContext = context;
            StartNote = startNote;
            EndNote = endNote;
            Now = now;
        }

        public Graphics RenderContext { get; }

        public Note StartNote { get; }

        public Note EndNote { get; }

        public void Initialize() {
            if (_isInitialized) {
                return;
            }
            BuildVertices();
            BuildTriangles();
            _isInitialized = true;
        }

        public void Fill(Brush brush) {
            var context = RenderContext;

            context.FillMesh(brush, _triangles);
        }

        protected abstract void BuildVertices();

        protected double Now { get; }

        private void BuildTriangles() {
            for (var i = 0; i < JointCount - 1; ++i) {
                var t1 = new Triangle();
                var t2 = new Triangle();
                Vector2 v1 = Vertices[i * 2], v2 = Vertices[i * 2 + 1], v3 = Vertices[i * 2 + 3], v4 = Vertices[i * 2 + 2];
                t1.Point1 = v1;
                t1.Point2 = v2;
                t1.Point3 = v4;
                t2.Point1 = v2;
                t2.Point2 = v3;
                t2.Point3 = v4;
                _triangles[i * 2] = t1;
                _triangles[i * 2 + 1] = t2;
            }
        }

        private static readonly Color[] RainbowColors = {
            new Color(0.8f, 1f, 0.65f, 0.65f),
            new Color(0.8f, 0.65f, 1f, 0.65f),
            new Color(0.8f, 0.65f, 0.65f, 1f)
        };

        private static readonly Color[] CoolColors = {
            new Color(0.8f, 0.211764708f, 0.929411769f, 0.992156863f),
            new Color(0.8f, 0.164705887f, 0.478431374f, 1f)
        };

        private static readonly Color[] CuteColors = {
            new Color(0.8f, 0.921568632f, 0.6784314f, 0.694117665f),
            new Color(0.8f, 1f, 0.3019608f, 0.533333361f)
        };

        private static readonly Color[] PassionColors = {
            new Color(0.8f, 1f, 0.827451f, 0.129411772f),
            new Color(0.8f, 0.854901969f, 0.5647059f, 0f)
        };

        private static readonly Color[] SimpleWhiteColors = {
            new Color(0.8f, 0.784313738f, 0.784313738f, 0.784313738f),
            new Color(0.8f, 1f, 1f, 1f)
        };

        protected const int JointCount = 48;

        protected readonly Vector2[] Vertices = new Vector2[JointCount * 2];

        private readonly Triangle[] _triangles = new Triangle[2 * (JointCount - 1)];

        private bool _isInitialized;

    }
}
