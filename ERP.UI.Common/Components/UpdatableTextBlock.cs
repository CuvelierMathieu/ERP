using ERP.UI.Common.Mediators;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ERP.UI.Common.Components
{
    public class UpdatableTextBlock : TextBlock
    {
        static readonly DependencyProperty MediatorProperty = DependencyProperty.Register(
            nameof(Mediator),
            typeof(IBindingUpdateMediator),
            typeof(UpdatableTextBlock),
            new(OnMediatorPropertyChanged));

        public IBindingUpdateMediator Mediator
        {
            get => GetValue(MediatorProperty) as IBindingUpdateMediator;
            set => SetValue(MediatorProperty, value);
        }

        private static void OnMediatorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not UpdatableTextBlock updatableTextBlock)
                return;

            updatableTextBlock.OnMediatorChanged(e.OldValue, e.NewValue);
        }

        private void OnMediatorChanged(object oldValue, object newValue)
        {
            if (oldValue is IBindingUpdateMediator oldMediator)
                oldMediator.OnUpdate -= MediatorCalledUpdate;

            if (newValue is IBindingUpdateMediator newMediator)
                newMediator.OnUpdate += MediatorCalledUpdate;
        }

        private void MediatorCalledUpdate()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            BindingExpression bindingToTextProperty = GetBindingExpression(TextProperty);
            bindingToTextProperty?.UpdateTarget();
        }
    }
}
