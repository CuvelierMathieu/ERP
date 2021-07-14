using ERP.Common.Models;
using ERP.Models.Margin;
using ERP.UI.Common.Mediators;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ERP.UI.Margin
{
    public class MarginViewModel : BaseModel
    {
        public ObservableCollection<Ingredient> Ingredients { get => Get(); private set => Set(value); }

        public IBindingUpdateMediator UpdateTotalMediator { get; private set; }

        public MarginViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            UpdateTotalMediator = new UpdateTotalMediator();

            LinkIngredientsPriceToMediator();

            Ingredients = new()
            {
                new() { Name = "Sel", Price = 1.2 },
                new() { Name = "Poivre", Price = 0.34 },
            };
        }

        private void LinkIngredientsPriceToMediator()
        {
            PropertyChanged += OnIngredientListChanged;
        }

        private void OnIngredientListChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName != nameof(Ingredients)
                || Ingredients is null)
                return;

            Ingredients.CollectionChanged += Ingredients_CollectionChanged;

            foreach (Ingredient item in Ingredients)
                item.PropertyChanged += UpdateTotalWhenPriceIsChanged;
        }

        private void Ingredients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (Ingredient item in e.OldItems)
                item.PropertyChanged -= UpdateTotalWhenPriceIsChanged;

            foreach (Ingredient item in e.NewItems)
                item.PropertyChanged += UpdateTotalWhenPriceIsChanged;
        }

        private void UpdateTotalWhenPriceIsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName != nameof(Ingredient.Price))
                return;

            UpdateTotalMediator.Update();
        }
    }

    public class UpdateTotalMediator : IBindingUpdateMediator
    {
        public event Action OnUpdate;

        public void Update() => OnUpdate?.Invoke();
    }
}
