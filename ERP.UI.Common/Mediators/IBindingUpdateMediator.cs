using System;

namespace ERP.UI.Common.Mediators
{
    public interface IBindingUpdateMediator
    {
        event Action OnUpdate;

        void Update();
    }
}
