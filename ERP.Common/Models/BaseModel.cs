using ERP.Common.Helpers;
using ERP.Common.Helpers.Builders;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ERP.Common.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        private IPropertyValueManager propertyValueManager;

        public event PropertyChangedEventHandler? PropertyChanged;

#nullable disable warnings
        protected BaseModel()
#nullable restore warnings
        {
            InitializePropertyValueManager();
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

#nullable disable warnings
        protected dynamic Get([CallerMemberName] string propertyName = null)
#nullable restore warnings
        {
            return propertyValueManager.Get(propertyName);
        }

#nullable disable warnings
        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
#nullable restore warnings
        {
            T previousValue = Get(propertyName);

            if ((previousValue is null && value is null)
                || (previousValue is not null && previousValue.Equals(value)))
                return;

            propertyValueManager.Set(value, propertyName);
            RaisePropertyChanged(propertyName);
        }

        private void InitializePropertyValueManager()
        {
            Type currentType = GetType();
            propertyValueManager = PropertyValueManagerBuilder.BuildForType(currentType);
        }
    }
}
