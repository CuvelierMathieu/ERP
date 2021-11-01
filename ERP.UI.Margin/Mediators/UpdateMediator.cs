using ERP.UI.Common.Mediators;
using System;

namespace ERP.UI.Margin.Mediators
{
    public class UpdateMediator : IBindingUpdateMediator
    {
        public event Action? OnUpdate;

        public void Update() => OnUpdate?.Invoke();
    }
}
