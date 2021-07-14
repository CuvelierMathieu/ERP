using ERP.Common.Helpers.Types;
using ERP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ERP.Common.Helpers
{
    public class PropertyValueManager<T> : IPropertyValueManager where T : BaseModel
    {
        private readonly Dictionary<string, object> valuesDictionary;
        private readonly object _lock = new();

        public PropertyValueManager()
        {
            valuesDictionary = new();
        }

        public dynamic Get(string propertyName)
        {
            if (PropertyIsNotInDictionary(propertyName))
                lock (_lock)
                    if (PropertyIsNotInDictionary(propertyName))
                        AddPropertyIntoDictionaryWithDefaultValue(propertyName);

            return ReadValueFromDictionary(propertyName);
        }

        public void Set<PropertyType>(PropertyType value, string propertyName)
        {
            valuesDictionary[propertyName] = value;
        }

        private bool PropertyIsNotInDictionary(string propertyName)
        {
            return !PropertyIsInDictionary(propertyName);
        }

        private bool PropertyIsInDictionary(string propertyName)
        {
            return valuesDictionary.ContainsKey(propertyName);
        }

        private void AddPropertyIntoDictionaryWithDefaultValue(string propertyName)
        {
            dynamic defaultValue = PropertyValueManager<T>.GetDefaultValueForProperty(propertyName);
            valuesDictionary.Add(propertyName, defaultValue);
        }

        private static dynamic GetDefaultValueForProperty(string propertyName)
        {
            PropertyInfo[] allProperties = ReflectionCache.GetPropertiesForType(typeof(T));
            PropertyInfo property = allProperties.Single(p => p.Name == propertyName);
            Type type = property.PropertyType;

            if (type.IsValueType)
            {
                object defaultValue = Activator.CreateInstance(type);

                return defaultValue;
            }
            else
            {
                ITypeHandler typeHandler = TypeHandlerBuilder.Build(type);
                object defaultValue = typeHandler.GetDefaultValue();

                return defaultValue;
            }
        }

        private dynamic ReadValueFromDictionary(string propertyName)
        {
            return valuesDictionary[propertyName];
        }
    }
}
