﻿using System;

namespace ERP.Common.Helpers.Types.Handlers
{
    public class DateTimeHandler : ITypeHandler
    {
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
