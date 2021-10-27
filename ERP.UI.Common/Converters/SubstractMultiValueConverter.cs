using ERP.Common.Helpers.Types;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ERP.UI.Common.Converters
{
    public class SubstractMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[]? values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values is null || !values.Any())
                throw new ArgumentNullException(nameof(values));

            if (values.Length == 1)
                throw new ArgumentException("Converter has to be used with more than one value", nameof(values));

            IEnumerator enumerator = values.GetEnumerator();
            enumerator.MoveNext();

            double result = 0;
            ITypeHandler typeHandler;

            if (enumerator.Current is not null)
            {
                typeHandler = TypeHandlerBuilder.Build(enumerator.Current.GetType());
                result = typeHandler.ConvertToDouble(enumerator.Current);
            }

            while (enumerator.MoveNext())
            {
                if (enumerator.Current is null)
                    continue;

                typeHandler = TypeHandlerBuilder.Build(enumerator.Current.GetType());
                double value = typeHandler.ConvertToDouble(enumerator.Current);
                result -= value;
            }

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
