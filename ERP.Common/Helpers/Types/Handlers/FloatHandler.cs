namespace ERP.Common.Helpers.Types.Handlers
{
    public class FloatHandler : ITypeHandler
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public object ConvertFromDouble(double value)
        {
            logger.Debug("Converting {double} from double to float", value);

            float result = (float)value;
            logger.Trace("Converted double {double} into int {int}", value, result);

            return result;
        }

        public double ConvertToDouble(object value)
        {
            logger.Debug("Converting {double} from float to double", value);
            return (float)value;
        }

        public object GetDefaultValue()
        {
            logger.Trace("Getting default float value");
            return default(float);
        }
    }
}
