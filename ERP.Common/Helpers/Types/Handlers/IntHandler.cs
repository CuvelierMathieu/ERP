using System;

namespace ERP.Common.Helpers.Types.Handlers
{
    public class IntHandler : ITypeHandler
    {
        public object ConvertFromDouble(double value)
        {
            return (int)Math.Round(value);
        }

        public double ConvertToDouble(object value)
        {
            return (int)value;
        }

        public object GetDefaultValue()
        {
            return default(int);
        }
    }
}
