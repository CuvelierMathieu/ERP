using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ERP.UI.Common.Converters
{
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public ThicknessPart ThicknessPart { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Thickness thickness)
                return DependencyProperty.UnsetValue;

            return thickness.GetPart(ThicknessPart);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
