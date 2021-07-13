using System;
using System.Collections.Generic;
using System.Reflection;

namespace ERP.Common.Helpers
{
    public static class ReflectionCache
    {
        private static readonly Dictionary<Type, PropertyInfo[]> propertiesDictionary = new();
        private static readonly object _lock = new();

        public static PropertyInfo[] GetPropertiesForType(Type type)
        {
            if (PropertiesAreNotInCacheForType(type))
                lock (_lock)
                    if (PropertiesAreNotInCacheForType(type))
                        StorePropertiesInCacheForType(type);

            return propertiesDictionary[type];
        }

        private static bool PropertiesAreNotInCacheForType(Type type)
        {
            return !PropertiesAreInCacheForType(type);
        }

        private static bool PropertiesAreInCacheForType(Type type)
        {
            return propertiesDictionary.ContainsKey(type);
        }

        private static void StorePropertiesInCacheForType(Type type)
        {
            propertiesDictionary[type] = type.GetProperties();
        }
    }
}
