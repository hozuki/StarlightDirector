using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap.Serialization;

namespace OpenCGSS.Director.Modules.SldProject.Models.Beatmap.Extensions {
    public static class CompiledScoreExtensions {

        [NotNull]
        public static string GetCsvString([NotNull] this CompiledScore score) {
            var sb = new StringBuilder();

            using (var writer = new StringWriter(sb)) {
                var config = new CsvConfiguration();

                config.RegisterClassMap<ScoreCsvMap>();
                config.HasHeaderRecord = true;
                config.TrimFields = false;

                using (var csv = new CsvWriter(writer, config)) {
                    var newList = new List<CompiledNote>(score.Notes);

                    csv.WriteRecords(newList);
                }
            }

            sb.Replace("\r\n", "\n");
            sb.Replace('\r', '\n');

            return sb.ToString();
        }

    }
}
