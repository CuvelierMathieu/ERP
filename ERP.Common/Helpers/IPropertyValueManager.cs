namespace ERP.Common.Helpers
{
    public interface IPropertyValueManager
    {
        dynamic Get(string propertyName);

        void Set<PropertyType>(PropertyType value, string propertyName);
    }
}