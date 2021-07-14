using ERP.Common.Models;
using System;

namespace ERP.Common.Helpers.Builders
{
    public static class PropertyValueManagerBuilder
    {
        public static IPropertyValueManager BuildForType(Type type)
        {
            Type baseType = typeof(PropertyValueManager<BaseModel>);
            Type genericDefinition = baseType.GetGenericTypeDefinition();
            Type aimedType = genericDefinition.MakeGenericType(type);

            IPropertyValueManager defaultValue = (IPropertyValueManager)Activator.CreateInstance(aimedType);

            return defaultValue;
        }
    }
}
