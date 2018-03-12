using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using OpenCGSS.Director.Modules.SldProject.Models;

namespace OpenCGSS.Director.Modules.SldProject.Converters {
    public sealed class EditModeToBooleanConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return false;
            }

            if (parameter == null) {
                return false;
            }

            Debug.Assert(value is ScoreEditMode);
            Debug.Assert(parameter is ScoreEditMode);

            var e = (ScoreEditMode)value;
            var p = (ScoreEditMode)parameter;

            return e == p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }

    }
}
