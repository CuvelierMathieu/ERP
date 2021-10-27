using ERP.Common.Helpers.Types;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ERP.UI.Common.Converters
{
    public class DividerMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is null || !values.Any())
                throw new ArgumentNullException(nameof(values));

            if (values.Length != 2)
                throw new ArgumentException("Converter can only convert exactly two values", nameof(values));

            if (values.Any(v => v is null
            || (v is string stringValue && string.IsNullOrWhiteSpace(stringValue))))
                return new();

            Type firstValueType = values[0].GetType();
            Type secondValueType = values[1].GetType();

            ITypeHandler firstTypeHandler = TypeHandlerBuilder.Build(firstValueType);
            ITypeHandler secondTypeHandler = TypeHandlerBuilder.Build(secondValueType);

            double firstValue = firstTypeHandler.ConvertToDouble(values[0]);
            double secondValue = secondTypeHandler.ConvertToDouble(values[1]);

            double result = firstValue / secondValue;

            if (targetType == typeof(string))
                return result.ToString();

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
