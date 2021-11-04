namespace ERP.Common.Helpers.Types
{
    public interface ITypeHandler
    {
        object? GetDefaultValue();

        double ConvertToDouble(object value);

        object ConvertFromDouble(double value);
    }
}
