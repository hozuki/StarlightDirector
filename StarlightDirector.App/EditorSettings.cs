using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StarlightDirector.App {
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public sealed class EditorSettings {

        [DefaultValue(CurrentVersion)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int Version { get; internal set; } = CurrentVersion;

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ShowNoteIndicators { get; internal set; } = false;

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool InvertedScrolling { get; internal set; } = false;

        [DefaultValue(5)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int ScrollingSpeed { get; internal set; } = 5;

        // version 2

        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Language { get; internal set; }

        internal EditorSettings() {
        }

        private const int CurrentVersion = 2;

    }
}
