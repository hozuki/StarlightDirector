using System.ComponentModel;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenCGSS.StarlightDirector.Models;
using OpenCGSS.StarlightDirector.UI.Controls.Previewing;

namespace OpenCGSS.StarlightDirector.DirectorApplication {
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

        [DefaultValue(15)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int PreviewTimerInterval { get; private set; } = 15;

        [DefaultValue(PreviewTimingSynchronizationMode.Naive)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public PreviewTimingSynchronizationMode PreviewTimingSynchronizationMode { get; private set; } = PreviewTimingSynchronizationMode.Naive;

        // Version 4
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [NotNull]
        public string ExternalPreviewerFile { get; set; } = string.Empty;

        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [NotNull]
        public string ExternalPreviwerArgs { get; set; } = string.Empty;

        private const int CurrentVersion = 4;

    }
}
