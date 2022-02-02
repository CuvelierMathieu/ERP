namespace ERP.Common.Helpers.Types.Handlers
{
    public class StringHandler : ITypeHandler
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public object ConvertFromDouble(double value)
        {
            logger.Debug("Converting {double} from double to string", value);
            return value.ToString();
        }

        public double ConvertToDouble(object value)
        {
            logger.Debug("Converting {double} from string to double", value);

            try
            {
                return double.Parse((string)value);
            }
            catch
            {
                return double.Parse(((string)value).Replace('.', ','));
            }
        }

        public object GetDefaultValue()
        {
            logger.Trace("Getting default string value");
            return string.Empty;
        }
    }
}
