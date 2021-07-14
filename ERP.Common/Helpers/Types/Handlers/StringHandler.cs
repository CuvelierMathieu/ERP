namespace ERP.Common.Helpers.Types.Handlers
{
    public class StringHandler : ITypeHandler
    {
        public object GetDefaultValue()
        {
            return default(string);
        }
    }
}
