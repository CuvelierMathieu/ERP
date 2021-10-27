using ERP.Models.Margin;
using ERP.UI.Margin;
using NUnit.Framework;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace ERP.UI.MarginTest
{
    [TestFixture]
    public class MarginViewModelTest
    {
        [Test]
        public void UpdateAPriceCallsTheMediator()
        {
            MarginViewModel viewModel = new();

            viewModel.Ingredients = new()
            {
                new() { Price = 22.03 }
            };

            bool hasBeenCalled = false;
            if (viewModel.UpdateTotalMediator is not null)
                viewModel.UpdateTotalMediator.OnUpdate += UpdateTotalMediator_OnUpdate;

            viewModel.Ingredients.Single().Price += 3;

            Assert.True(hasBeenCalled);

            void UpdateTotalMediator_OnUpdate()
            {
                hasBeenCalled = true;
            }
        }

        [Test]
        public void AddAnIngredientCallsTheMediator()
        {
            MarginViewModel viewModel = new();

            viewModel.Ingredients = new()
            {
                new() { Price = 22.03 }
            };

            bool hasBeenCalled = false;
            if (viewModel.UpdateTotalMediator is not null)
                viewModel.UpdateTotalMediator.OnUpdate += UpdateTotalMediator_OnUpdate;

            viewModel.Ingredients.Add(new() { Price = -8 });

            Assert.True(hasBeenCalled);

            void UpdateTotalMediator_OnUpdate()
            {
                hasBeenCalled = true;
            }
        }

        [Test]
        public void RemoveAnIngredientCallsTheMediator()
        {
            MarginViewModel viewModel = new();

            viewModel.Ingredients = new()
            {
                new() { Price = 22.03 },
                new() { Price = 1988 }
            };

            bool hasBeenCalled = false;
            if (viewModel.UpdateTotalMediator is not null)
                viewModel.UpdateTotalMediator.OnUpdate += UpdateTotalMediator_OnUpdate;

            viewModel.Ingredients.RemoveAt(1);

            Assert.True(hasBeenCalled);

            void UpdateTotalMediator_OnUpdate()
            {
                hasBeenCalled = true;
            }
        }

        [Test]
        public void AddCommandAddsAnIngredientToList()
        {
            MarginViewModel viewModel = new();
            int quantityOfIngredientsAdded = 0,
                expected = 1;
            viewModel.Ingredients.CollectionChanged += Ingredients_CollectionChanged;

            viewModel.AddCommand?.Execute(null);

            Assert.AreEqual(expected, quantityOfIngredientsAdded);

            void Ingredients_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            {
                quantityOfIngredientsAdded += e.NewItems?.Count ?? 0;
            }
        }

        [Test]
        public void ChangePriceOfANewlyAddedIngredientCallsTheMediator()
        {
            MarginViewModel viewModel = new();
            Ingredient addedIngredient = new();
            viewModel.Ingredients.CollectionChanged += Ingredients_CollectionChanged;
            viewModel.AddCommand?.Execute(null);
            bool mediatorHasBeenCalled = false; 
            
            if (viewModel.UpdateTotalMediator is not null)
                viewModel.UpdateTotalMediator.OnUpdate += UpdateTotalMediator_OnUpdate;

            addedIngredient.Price += 3;

            Assert.True(mediatorHasBeenCalled);

            void Ingredients_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.NewItems is not null && e.NewItems[0] is Ingredient ingredient)
                    addedIngredient = ingredient;
            }

            void UpdateTotalMediator_OnUpdate()
            {
                mediatorHasBeenCalled = true;
            }
        }

        [Test]
        public void DeleteCommandRemovesIngredientFromList()
        {
            MarginViewModel viewModel = new();
            Ingredient ingredient = new()
            {
                Name = Guid.NewGuid().ToString()
            };

            viewModel.Ingredients.Add(ingredient);

            viewModel.DeleteCommand?.Execute(ingredient);

            Assert.False(viewModel.Ingredients.Any(i => i.Name == ingredient.Name));
        }

        [Test]
        public void ChangePriceOfADeletedIngredientDoesntCallTheMediator()
        {
            MarginViewModel viewModel = new();
            Ingredient ingredient = new()
            {
                Name = Guid.NewGuid().ToString()
            };

            viewModel.Ingredients.Add(ingredient);
            viewModel.DeleteCommand?.Execute(ingredient);

            if (viewModel.UpdateTotalMediator is not null)
                viewModel.UpdateTotalMediator.OnUpdate += UpdateTotalMediator_OnUpdate;

            ingredient.Price += 4;

            static void UpdateTotalMediator_OnUpdate()
            {
                Assert.Fail();
            }
        }

        [Test]
        public void DeleteAnIngredientCallsTheMediator()
        {
            MarginViewModel viewModel = new();
            Ingredient ingredient = new()
            {
                Name = Guid.NewGuid().ToString()
            };

            viewModel.Ingredients.Add(ingredient);
            bool mediatorHasBeenCalled = false;

            if (viewModel.UpdateTotalMediator is not null)
                viewModel.UpdateTotalMediator.OnUpdate += UpdateTotalMediator_OnUpdate;

            viewModel.DeleteCommand?.Execute(ingredient);

            Assert.True(mediatorHasBeenCalled);

            void UpdateTotalMediator_OnUpdate()
            {
                mediatorHasBeenCalled = true;
            }
        }
    }
}
