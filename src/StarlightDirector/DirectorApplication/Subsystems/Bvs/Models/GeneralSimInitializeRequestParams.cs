using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs.Models.Proposals;

namespace OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs.Models {
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class GeneralSimInitializeRequestParams {

        [JsonConstructor]
        internal GeneralSimInitializeRequestParams() {
        }

        [NotNull, ItemNotNull]
        public SupportedFormatDescriptor[] SupportedFormats { get; set; }

    }
}
