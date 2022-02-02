using ERP.Common.Models;
using System;

namespace ERP.Common.Helpers.Builders
{
    public static class PropertyValueManagerBuilder
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static IPropertyValueManager BuildForType(Type type)
        {
            logger.Trace("Building property value manager for type {Type}", type);

            Type baseType = typeof(PropertyValueManager<BaseModel>);
            Type genericDefinition = baseType.GetGenericTypeDefinition();
            Type aimedType = genericDefinition.MakeGenericType(type);
            object? createdInstance = Activator.CreateInstance(aimedType);

            if (createdInstance is null)
                throw new ArgumentException($"Failed to create an instance for type {type}.");

            IPropertyValueManager defaultValue = (IPropertyValueManager)createdInstance;

            return defaultValue;
        }
    }
}
