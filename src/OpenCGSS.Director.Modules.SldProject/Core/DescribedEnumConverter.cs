using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace OpenCGSS.Director.Modules.SldProject.Core {
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
            var valueName = Enum.GetName(_enumType, value);
            if (valueName != null) {
                var fi = _enumType.GetField(valueName);
                var descriptionAttribute = fi.GetCustomAttribute<DescriptionAttribute>();
                return descriptionAttribute?.Description ?? value.ToString();
            } else {
                var intValue = (int)value;
                var allEnumValues = Enum.GetValues(_enumType);
                var names = new List<string>();
                foreach (var enumValue in allEnumValues) {
                    var v = (int)enumValue;
                    if ((intValue & v) != 0) {
                        var s = (string)ConvertTo(context, culture, v, destType);
                        names.Add(s);
                    }
                }
                var finalString = names.Aggregate((f, v) => f == null ? v : f + ", " + v);
                return finalString;
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType) {
            return srcType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            // Get value.
            var stringValue = (string)value;
            foreach (var fi in _enumType.GetFields()) {
                var dna = fi.GetCustomAttribute<DescriptionAttribute>();
                if (dna != null && stringValue == dna.Description) {
                    return Enum.Parse(_enumType, fi.Name);
                }
            }
            return string.IsNullOrEmpty(stringValue) ? 0 : Enum.Parse(_enumType, stringValue);
        }

        public static string GetEnumDescription(Enum value) {
            return GetEnumDescription(value, value.GetType());
        }

        public static string GetEnumDescription(Enum value, Type enumType) {
            var converter = new DescribedEnumConverter(enumType);
            return converter.ConvertToInvariantString(value);
        }

        private readonly Type _enumType;

    }
}
