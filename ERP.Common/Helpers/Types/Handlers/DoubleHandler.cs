namespace ERP.Common.Helpers.Types.Handlers
{
    public class DoubleHandler : ITypeHandler
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public object ConvertFromDouble(double value)
        {
            return value;
        }

        public double ConvertToDouble(object value)
        {
            return (double)value;
        }

        public object GetDefaultValue()
        {
            return default(double);
        }
    }
}
