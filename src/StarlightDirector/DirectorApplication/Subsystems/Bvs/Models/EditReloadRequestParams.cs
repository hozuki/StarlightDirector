using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs.Models {
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class EditReloadRequestParams {

        [JsonProperty(Required = Required.AllowNull)]
        [CanBeNull]
        public string BeatmapFile { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int BeatmapIndex { get; set; }

        [JsonProperty(Required = Required.Always)]
        public float BeatmapOffset { get; set; }

        [JsonProperty]
        [CanBeNull]
        public string BackgroundMusicFile { get; set; }

        [JsonProperty]
        [CanBeNull]
        public string BackgroundVideoFile { get; set; }

    }
}
