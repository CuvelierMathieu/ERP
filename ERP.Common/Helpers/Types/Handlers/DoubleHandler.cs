namespace ERP.Common.Helpers.Types.Handlers
{
    public class DoubleHandler : ITypeHandler
    {
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
