namespace ERP.Common.Helpers.Types.Handlers
{
    public class IntHandler : ITypeHandler
    {
        public object GetDefaultValue()
        {
            return default(int);
        }
    }
}
