using System;
using System.ComponentModel;
using System.Globalization;

namespace StarlightDirector.Beatmap {
    public sealed class DescribedEnumConverter : EnumConverter {

        public DescribedEnumConverter(Type type)
            : base(type) {
            _enumType = type;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) {
            return destType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType) {
            // Get description.
            if (value == null) {
                throw new ArgumentException("Described enum cannot be null.", nameof(value));
            }
            var fi = _enumType.GetField(Enum.GetName(_enumType, value));
            var dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
            return dna != null ? dna.Description : value.ToString();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType) {
            return srcType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            // Get value.
            var stringValue = (string)value;
            foreach (var fi in _enumType.GetFields()) {
                var dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                if (dna != null && stringValue == dna.Description) {
                    return Enum.Parse(_enumType, fi.Name);
                }
            }
            return string.IsNullOrEmpty(stringValue) ? 0 : Enum.Parse(_enumType, stringValue);
        }

        private readonly Type _enumType;

    }
}
