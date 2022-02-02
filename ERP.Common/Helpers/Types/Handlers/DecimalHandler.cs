namespace ERP.Common.Helpers.Types.Handlers
{
    public class DecimalHandler : ITypeHandler
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public object ConvertFromDouble(double value)
        {
            logger.Debug("Converting {double} from double to decimal", value);
            
            decimal result = (decimal)(double)value;
            logger.Trace("Converted double {double} into decimal {decimal}", value, result);

            return result;
        }

        public double ConvertToDouble(object value)
        {
            logger.Debug("Converting {double} from decimal to double", value);
            
            double result = (double)(decimal)value;
            logger.Trace("Converted decimal {decimal} into double {double}", value, result);
            
            return result;
        }

        public object GetDefaultValue()
        {
            logger.Trace("Getting default decimal value");
            return default(decimal);
        }
    }
}
