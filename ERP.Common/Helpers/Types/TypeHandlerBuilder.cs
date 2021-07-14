using ERP.Common.Helpers.Types.Handlers;
using System;

namespace ERP.Common.Helpers.Types
{
    public static class TypeHandlerBuilder
    {
        public static ITypeHandler Build<T>()
        {
            return Build(typeof(T));
        }

        public static ITypeHandler Build(Type type)
        {
            if (type == typeof(string))
                return new StringHandler();

            if (type == typeof(double))
                return new DoubleHandler();

            if (type == typeof(int))
                return new IntHandler();

            if (type == typeof(DateTime))
                return new DateTimeHandler();

            throw new NotImplementedException($"Type {type} is not handled");
        }
    }
}
