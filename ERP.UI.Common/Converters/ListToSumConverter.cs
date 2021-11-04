using ERP.Common.Helpers.Types;
using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using Unity;

namespace ERP.UI.Common.Converters
{
    public class ListToSumConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return 0;

            if (value is string
                || value is not IEnumerable list)
                throw new ArgumentException("Value is expected to be a IEnumerable.");

            double sum = 0;
            IEnumerator enumerator = list.GetEnumerator();
            PropertyInfo? propertyInfo = null;
            ITypeHandler? typeHandler = null;

            while (enumerator.MoveNext())
            {
                object? current = enumerator.Current;

                if (propertyInfo is not null)
                    current = propertyInfo.GetValue(current);
                else if (parameter is string stringParameter)
                {
                    if (string.IsNullOrWhiteSpace(stringParameter))
                        throw new ArgumentException("Parameter is empty or white space.");

                    propertyInfo = enumerator.Current.GetType().GetProperty(stringParameter);

                    if (propertyInfo is null)
                        throw new ArgumentException("Parameter does not match any of the value properties.");

                    current = propertyInfo.GetValue(current);
                }

                if (current is null)
                    continue;

                try
                {
                    if (typeHandler is null && current is not null)
                        typeHandler = TypeHandlerBuilder.Build(current.GetType());
                }
                catch (ResolutionFailedException exception)
                {
                    throw new ArgumentException("Current item type is not handled and parameter is null.", exception);
                }
                catch
                {
                    throw;
                }

                if (typeHandler is null)
                    throw new InvalidOperationException("Type handler has not been initialized.");

#nullable disable warnings
                double currentAsDouble = typeHandler.ConvertToDouble(current);
#nullable restore warnings

                sum += currentAsDouble;
            }

            ITypeHandler targetTypeHandler = TypeHandlerBuilder.Build(targetType);

            return targetTypeHandler.ConvertFromDouble(sum);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
