using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StarlightDirector.UI.Controls.Previewing;

namespace StarlightDirector.App {
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public sealed class DirectorSettings {

        internal DirectorSettings() {
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
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

        // Version 3
        [DefaultValue(PreviewerRenderMode.Standard)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public PreviewerRenderMode PreviewRenderMode { get; internal set; } = PreviewerRenderMode.Standard;

        [DefaultValue(9)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public float PreviewSpeed { get; internal set; } = 9;

        private const int CurrentVersion = 3;

    }
}
