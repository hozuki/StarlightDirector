using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenCGSS.Director.Modules.SldProject.Converters.MathOp {
    public abstract class MathConverterBase : IValueConverter {

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public static double GetObjectValue(object value) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return 0d;
            }

            if (value is double d) {
                return d;
            } else if (value is string) {
                double.TryParse((string)value, out d);
                return d;
            } else {
                return System.Convert.ToDouble(value);
            }
        }

    }
}
