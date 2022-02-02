using System;

namespace ERP.Common.Helpers.Types.Handlers
{
    public class IntHandler : ITypeHandler
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public object ConvertFromDouble(double value)
        {
            logger.Debug("Converting {double} from double to int", value);

            int result = (int)Math.Round(value);
            logger.Trace("Converted double {double} into int {int}", value, result);
            
            return result;
        }

        public double ConvertToDouble(object value)
        {
            logger.Debug("Converting {double} from int to double", value);
            return (int)value;
        }

        public object GetDefaultValue()
        {
            logger.Trace("Getting default int value");
            return default(int);
        }
    }
}
