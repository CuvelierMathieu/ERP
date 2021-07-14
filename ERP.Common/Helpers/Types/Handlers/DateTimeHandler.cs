using System;

namespace ERP.Common.Helpers.Types.Handlers
{
    public class DateTimeHandler : ITypeHandler
    {
        public object GetDefaultValue()
        {
            return default(DateTime);
        }
    }
}
