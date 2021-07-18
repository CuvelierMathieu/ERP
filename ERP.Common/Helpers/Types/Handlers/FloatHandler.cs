using System;

namespace ERP.Common.Helpers.Types.Handlers
{
    public class FloatHandler : ITypeHandler
    {
        public double ConvertToDouble(object value)
        {
            return (float)value;
        }

        public object GetDefaultValue()
        {
            return default(float);
        }
    }
}
