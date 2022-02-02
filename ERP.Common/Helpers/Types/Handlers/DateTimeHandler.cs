using System;

namespace ERP.Common.Helpers.Types.Handlers
{
    public class DateTimeHandler : ITypeHandler
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

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
            logger.Trace("Getting default DateTime value");
            return default(DateTime);
        }
    }
}
