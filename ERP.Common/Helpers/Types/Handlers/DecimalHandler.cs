namespace ERP.Common.Helpers.Types.Handlers
{
    public class DecimalHandler : ITypeHandler
    {
        public object ConvertFromDouble(double value)
        {
            return (decimal)(double)value;
        }

        public double ConvertToDouble(object value)
        {
            return (double)(decimal)value;
        }

        public object GetDefaultValue()
        {
            return default(decimal);
        }
    }
}
