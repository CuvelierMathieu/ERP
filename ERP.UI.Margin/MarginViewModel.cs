using ERP.Common.Models;
using ERP.Models.Margin;
using ERP.UI.Common.Commands;
using ERP.UI.Common.Mediators;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;

namespace ERP.UI.Margin
{
    public class MarginViewModel : BaseModel
    {
        public ObservableCollection<Ingredient> Ingredients { get => Get(); set => Set(value); }

        public IBindingUpdateMediator? UpdateTotalMediator { get; private set; }
        
        public ICommand? AddCommand { get; set; }

        public ICommand? DeleteCommand { get; set; }

        public double SellPrice { get => Get(); set => Set(value); }

        public MarginViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            UpdateTotalMediator = new UpdateTotalMediator();

            LinkIngredientsPriceToMediator();
            InitializeCommands();

            Ingredients = new();
        }

        private void InitializeCommands()
        {
            AddCommand = CommandBuilder.Build(AddCommandExecute);
            DeleteCommand = CommandBuilder.BuildWithParameter<Ingredient>(DeleteCommandExecute);
        }

        private void LinkIngredientsPriceToMediator()
        {
            PropertyChanged += OnIngredientListChanged;
        }

        private void OnIngredientListChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName != nameof(Ingredients)
                || Ingredients is null)
                return;

            Ingredients.CollectionChanged += Ingredients_CollectionChanged;

            foreach (Ingredient item in Ingredients)
                item.PropertyChanged += UpdateTotalWhenPriceIsChanged;
        }

        private void Ingredients_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems is not null)
                foreach (Ingredient item in e.OldItems)
                    item.PropertyChanged -= UpdateTotalWhenPriceIsChanged;

            if (e.NewItems is not null)
                foreach (Ingredient item in e.NewItems)
                    item.PropertyChanged += UpdateTotalWhenPriceIsChanged;

            UpdateTotalMediator?.Update();
        }

        private void UpdateTotalWhenPriceIsChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName != nameof(Ingredient.Price))
                return;

            UpdateTotalMediator?.Update();
        }

        private void AddCommandExecute()
        {
            Ingredient ingredient = new();
            Ingredients.Add(ingredient);
        }

        private void DeleteCommandExecute(Ingredient? ingredient)
        {
            if (ingredient is not null)
                _ = Ingredients.Remove(ingredient);
        }
    }

    public class UpdateTotalMediator : IBindingUpdateMediator
    {
        public event Action? OnUpdate;

        public void Update() => OnUpdate?.Invoke();
    }
}
