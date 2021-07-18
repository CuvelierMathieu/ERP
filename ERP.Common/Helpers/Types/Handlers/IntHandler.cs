﻿namespace ERP.Common.Helpers.Types.Handlers
{
    public class IntHandler : ITypeHandler
    {
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
