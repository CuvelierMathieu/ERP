using System;

namespace ERP.Common.Helpers.Types.Handlers
{
    public class DateTimeHandler : ITypeHandler
    {
        public object ConvertFromDouble(double value)
        {
            throw new NotImplementedException();
        }

        public double ConvertToDouble(object value)
        {
            throw new NotImplementedException();
        }

        public object GetDefaultValue()
        {
            return default(DateTime);
        }
    }
}
