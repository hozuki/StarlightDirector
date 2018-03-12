using System;
using System.Globalization;
using System.Windows;

namespace OpenCGSS.Director.Modules.SldProject.Converters.MathOp {
    public sealed class NegateConverter : UnaryOpConverterBase {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is double d) {
                return -d;
            } else if (value is bool b) {
                return !b;
            } else {
                return DependencyProperty.UnsetValue;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is double d) {
                return -d;
            } else if (value is bool b) {
                return !b;
            } else {
                return DependencyProperty.UnsetValue;
            }
        }

    }
}
