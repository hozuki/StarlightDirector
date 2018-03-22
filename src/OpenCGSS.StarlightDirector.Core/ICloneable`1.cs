using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector {
    public interface ICloneable<out T> {

        [NotNull]
        T Clone();

    }
}
