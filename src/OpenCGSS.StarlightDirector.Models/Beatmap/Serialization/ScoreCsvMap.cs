using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace OpenCGSS.StarlightDirector.Models.Beatmap.Serialization {
    public sealed class ScoreCsvMap : ClassMap<CompiledNote> {

        public ScoreCsvMap() {
            Map(m => m.ID).Name("id");
            Map(m => m.HitTiming).Name("sec").TypeConverter<RestrictedDoubleToStringConverter>();
            Map(m => m.Type).Name("type").TypeConverter<StringToIntConverter>();
            // See song_3034 (m063), master level score. These fields are empty, so we need a custom type converter.
            Map(m => m.StartPosition).Name("startPos").TypeConverter<StringToIntConverter>();
            Map(m => m.FinishPosition).Name("finishPos").TypeConverter<StringToIntConverter>();
            Map(m => m.FlickType).Name("status").TypeConverter<StringToIntConverter>();
            Map(m => m.IsSync).Name("sync").TypeConverter<IntToBoolConverter>();
            Map(m => m.GroupID).Name("groupId");
        }

        private sealed class IntToBoolConverter : ITypeConverter {

            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) {
                return (bool)value ? "1" : "0";
            }

            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) {
                if (string.IsNullOrEmpty(text)) {
                    return false;
                }
                var value = int.Parse(text);
                return value != 0;
            }
        }

        private sealed class RestrictedDoubleToStringConverter : ITypeConverter {

            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) {
                return ((double)value).ToString("0.######");
            }

            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) {
                if (string.IsNullOrEmpty(text)) {
                    return 0d;
                }

                return double.Parse(text);
            }

        }

        private sealed class StringToIntConverter : ITypeConverter {

            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) {
                // The conversion is for enums.
                return ((int)value).ToString();
            }

            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) {
                if (string.IsNullOrEmpty(text)) {
                    return 0;
                }
                var value = int.Parse(text);
                return value;
            }

        }

    }
}
