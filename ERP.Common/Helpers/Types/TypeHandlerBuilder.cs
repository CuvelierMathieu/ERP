using ERP.Common.Helpers.Types.Handlers;
using System;
using Unity;

namespace ERP.Common.Helpers.Types
{
    public static class TypeHandlerBuilder
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly IUnityContainer _container = GetContainer();

        private static IUnityContainer GetContainer()
        {
            logger.Debug("Registering type into container");
            IUnityContainer container = new UnityContainer();

            container.RegisterType<ITypeHandler, StringHandler>(name: typeof(string).FullName);
            container.RegisterType<ITypeHandler, DoubleHandler>(name: typeof(double).FullName);
            container.RegisterType<ITypeHandler, IntHandler>(name: typeof(int).FullName);
            container.RegisterType<ITypeHandler, DateTimeHandler>(name: typeof(DateTime).FullName);
            container.RegisterType<ITypeHandler, DecimalHandler>(name: typeof(decimal).FullName);
            container.RegisterType<ITypeHandler, FloatHandler>(name: typeof(float).FullName);

            return container;
        }

        public static ITypeHandler Build<T>()
        {
            return Build(typeof(T));
        }

        public static ITypeHandler Build(Type type)
        {
            logger.Debug("Building handler for type {Type}", type);
            return _container.Resolve<ITypeHandler>(type.FullName);
        }
    }
}
