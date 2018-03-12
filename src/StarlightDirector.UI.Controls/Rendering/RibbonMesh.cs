using System.Drawing;
using SharpDX.Direct2D1;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Rendering {
    internal abstract class RibbonMesh {

        public RibbonMesh(D2DRenderContext context, Note startNote, Note endNote, double now) {
            RenderContext = context;
            StartNote = startNote;
            EndNote = endNote;
            Now = now;
        }

        public D2DRenderContext RenderContext { get; }

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

        public void Fill(D2DBrush brush) {
            var context = RenderContext;
            var target = context.RenderTarget;
            using (var mesh = new D2DMesh(context, Triangles)) {
                // https://msdn.microsoft.com/en-us/library/dd371939.aspx
                target.AntialiasMode = AntialiasMode.Aliased;
                context.FillMesh(brush, mesh);
                target.AntialiasMode = AntialiasMode.PerPrimitive;
            }
        }

        protected abstract void BuildVertices();

        protected double Now { get; }

        private void BuildTriangles() {
            for (var i = 0; i < JointCount - 1; ++i) {
                var t1 = new D2DTriangle();
                var t2 = new D2DTriangle();
                PointF v1 = Vertices[i * 2], v2 = Vertices[i * 2 + 1], v3 = Vertices[i * 2 + 3], v4 = Vertices[i * 2 + 2];
                t1.Point1 = v1;
                t1.Point2 = v2;
                t1.Point3 = v4;
                t2.Point1 = v2;
                t2.Point2 = v3;
                t2.Point3 = v4;
                Triangles[i * 2] = t1;
                Triangles[i * 2 + 1] = t2;
            }
        }

        private static readonly Color[] RainbowColors = {
            D2DHelper.GetFloatColor(0.8f, 1f, 0.65f, 0.65f),
            D2DHelper.GetFloatColor(0.8f, 0.65f, 1f, 0.65f),
            D2DHelper.GetFloatColor(0.8f, 0.65f, 0.65f, 1f)
        };

        private static readonly Color[] CoolColors = {
            D2DHelper.GetFloatColor(0.8f, 0.211764708f, 0.929411769f, 0.992156863f),
            D2DHelper.GetFloatColor(0.8f, 0.164705887f, 0.478431374f, 1f)
        };

        private static readonly Color[] CuteColors = {
            D2DHelper.GetFloatColor(0.8f, 0.921568632f, 0.6784314f, 0.694117665f),
            D2DHelper.GetFloatColor(0.8f, 1f, 0.3019608f, 0.533333361f)
        };

        private static readonly Color[] PassionColors = {
            D2DHelper.GetFloatColor(0.8f, 1f, 0.827451f, 0.129411772f),
            D2DHelper.GetFloatColor(0.8f, 0.854901969f, 0.5647059f, 0f)
        };

        private static readonly Color[] SimpleWhiteColors = {
            D2DHelper.GetFloatColor(0.8f, 0.784313738f, 0.784313738f, 0.784313738f),
            D2DHelper.GetFloatColor(0.8f, 1f, 1f, 1f)
        };

        protected const int JointCount = 48;

        protected readonly PointF[] Vertices = new PointF[JointCount * 2];
        private readonly D2DTriangle[] Triangles = new D2DTriangle[2 * (JointCount - 1)];

        private bool _isInitialized;

    }
}