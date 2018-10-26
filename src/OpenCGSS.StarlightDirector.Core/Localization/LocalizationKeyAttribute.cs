using System;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Localization {
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class LocalizationKeyAttribute : Attribute {

        public LocalizationKeyAttribute([NotNull] string key) {
            Guard.ArgumentNotNull(key, nameof(key));

            Key = key;
        }

        [NotNull]
        public string Key { get; }

    }
}
