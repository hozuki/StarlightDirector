using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenCGSS.Director.Modules.SldProject.Converters {
    // http://stackoverflow.com/questions/1350598/passing-two-command-parameters-using-a-wpf-binding
    public sealed class EquityConverter : IMultiValueConverter {

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture) {
            if (value.Length != 2) {
                throw new ArgumentException("There should be 2 objects for EquityConverter.");
            }

            var b = EqualityComparer<object>.Default.Equals(value[0], value[1]);

            bool negate;

            if (ReferenceEquals(parameter, null) || parameter == DependencyProperty.UnsetValue) {
                negate = false;
            } else if (parameter is bool boolValue) {
                negate = boolValue;
            } else {
                Debug.Print("Unrecognized parameter type for EquityConverter: {0}, setting Negate=false.", parameter.GetType().FullName);

                negate = false;
            }

            if (negate) {
                b = !b;
            }

            return b;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }

        public static readonly bool Negate = true;
        public static readonly bool True = true;
        public static readonly bool False = false;

    }
}
