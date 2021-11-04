namespace ERP.Common.Helpers.Types.Handlers
{
    public class StringHandler : ITypeHandler
    {
        public object ConvertFromDouble(double value)
        {
            return value.ToString();
        }

        public double ConvertToDouble(object value)
        {
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
            return string.Empty;
        }
    }
}
