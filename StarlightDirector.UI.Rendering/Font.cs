using StarlightDirector.Core;

namespace StarlightDirector.UI.Rendering {
    public abstract class Font : DisposableBase {

        protected Font(string familyName, float size, FontStyle style, float weight) {
            FamilyName = familyName;
            Size = size;
            Style = style;
            Weight = weight;
        }

        public string FamilyName { get; }

        public float Size { get; }

        public FontStyle Style { get; }

        public float Weight { get; }

    }
}
