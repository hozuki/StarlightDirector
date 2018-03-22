using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Globalization {
    public sealed class DescribedEnumConverter : EnumConverter {

        public DescribedEnumConverter([NotNull] Type type)
            : base(type) {
            Guard.ArgumentNotNull(type, nameof(type));

            _enumType = type;
        }

        public DescribedEnumConverter([NotNull] Type type, [CanBeNull] LanguageManager languageManager)
            : base(type) {
            Guard.ArgumentNotNull(type, nameof(type));

            _enumType = type;
            _languageManager = languageManager;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) {
            return destType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType) {
            Guard.NotNull(value, nameof(value));

            // Get description.
            var valueName = Enum.GetName(_enumType, value);

            if (valueName != null) {
                var fieldInfo = _enumType.GetField(valueName);
                var localizationKeyAttribute = fieldInfo.GetCustomAttribute<LocalizationKeyAttribute>();

                if (localizationKeyAttribute?.Key != null) {
                    var languageManager = _languageManager ?? LanguageManager.Current;
                    var localizationValue = languageManager?.GetString(localizationKeyAttribute.Key, null);

                    if (localizationValue != null) {
                        return localizationValue;
                    }
                }

                var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

                return descriptionAttribute?.Description ?? value.ToString();
            } else {
                var intValue = (int)value;
                var allEnumValues = Enum.GetValues(_enumType);
                var allEnumNames = new List<string>();

                foreach (var enumValue in allEnumValues) {
                    var v = (int)enumValue;

                    if ((intValue & v) != 0) {
                        var s = (string)ConvertTo(context, culture, v, destType);

                        allEnumNames.Add(s);
                    }
                }

                var finalString = string.Join(", ", allEnumNames);

                return finalString;
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType) {
            return srcType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            // TODO: This method can only parse a single enum value.
            var stringValue = (string)value;

            foreach (var fieldInfo in _enumType.GetFields()) {
                var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

                if (descriptionAttribute != null && stringValue == descriptionAttribute.Description) {
                    return Enum.Parse(_enumType, fieldInfo.Name);
                }
            }

            return string.IsNullOrEmpty(stringValue) ? Enum.ToObject(_enumType, 0) : Enum.Parse(_enumType, stringValue);
        }

        [NotNull]
        public static string GetEnumDescription(Enum value) {
            return GetEnumDescription(value, value.GetType(), null);
        }

        [NotNull]
        public static string GetEnumDescription(Enum value, Type enumType) {
            var converter = new DescribedEnumConverter(enumType, null);
            var description = converter.ConvertToInvariantString(value);

            Trace.Assert(description != null, nameof(description) + " != null");

            return description;
        }

        [NotNull]
        public static string GetEnumDescription(Enum value, [NotNull] LanguageManager manager) {
            return GetEnumDescription(value, value.GetType(), manager);
        }

        [NotNull]
        public static string GetEnumDescription(Enum value, [NotNull] Type enumType, [CanBeNull] LanguageManager manager) {
            var converter = new DescribedEnumConverter(enumType, manager);
            var description = converter.ConvertToInvariantString(value);

            Trace.Assert(description != null, nameof(description) + " != null");

            return description;
        }

        [NotNull]
        private readonly Type _enumType;
        [CanBeNull]
        private readonly LanguageManager _languageManager;

    }
}
