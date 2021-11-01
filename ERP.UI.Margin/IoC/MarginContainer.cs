using ERP.UI.Common.Mediators;
using ERP.UI.Margin.Mediators;
using Unity;

namespace ERP.UI.Margin.IoC
{
    public class MarginContainer : UnityContainer, IUnityContainer
    {
        private static readonly object _lock = new();
        private static MarginContainer? _instance;

        public static MarginContainer Instance => GetInstance();
        
        private MarginContainer()
        {
            RegisterTypes();
        }

        private static MarginContainer GetInstance()
        {
            if (_instance is null)
                lock (_lock)
                    if (_instance is null)
                        _instance = new();

            return _instance;
        }

        private void RegisterTypes()
        {
            this.RegisterType<IBindingUpdateMediator, UpdateMediator>();
        }
    }
}
