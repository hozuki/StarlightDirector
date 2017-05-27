using System;

namespace StarlightDirector.Beatmap {
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class LocalizationKeyAttribute : Attribute {

        public LocalizationKeyAttribute(string key) {
            Key = key;
        }

        public string Key { get; }

    }
}
