using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace ERP.UI.Common.Converters
{
    public class ListToSumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return 0;

            if (value is string
                || value is not IEnumerable list)
                throw new ArgumentException("Value is expected to be a IEnumerable");

            double sum = 0;
            IEnumerator enumerator = list.GetEnumerator();
            PropertyInfo propertyInfo = null;

            while (enumerator.MoveNext())
            {
                object current = enumerator.Current;

                if (propertyInfo is not null)
                    current = propertyInfo.GetValue(current);
                else if (parameter is string stringParameter)
                {
                    if (string.IsNullOrWhiteSpace(stringParameter))
                        throw new ArgumentException("Parameter is empty or white space");

                    propertyInfo = enumerator.Current.GetType().GetProperty(stringParameter);
                    current = propertyInfo.GetValue(current);
                }

                if (current is double currentDoubleValue)
                    sum += currentDoubleValue;
                else if (current is int currentIntValue)
                    sum += currentIntValue;
                else if (current is decimal currentDecimalValue)
                    sum += double.Parse(currentDecimalValue.ToString());
                else if (parameter is null)
                    throw new ArgumentException("Current item type is not handled by converter and parameter is null");
            }

            if (targetType == typeof(string))
                return sum.ToString();

            if (targetType == typeof(double))
                return sum;

            if (targetType == typeof(decimal))
                return decimal.Parse(sum.ToString());

            if (targetType == typeof(int))
                return (int)Math.Round(sum);

            throw new NotImplementedException($"Converter is not handled to target type {targetType}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
