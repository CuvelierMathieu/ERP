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

        public event PropertyChangedEventHandler PropertyChanged;

        protected BaseModel()
        {
            InitializePropertyValueManager();
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        protected dynamic Get([CallerMemberName] string propertyName = null)
        {
            return propertyValueManager.Get(propertyName);
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
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
