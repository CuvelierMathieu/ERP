using ERP.UI.Margin;
using NUnit.Framework;
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
            viewModel.UpdateTotalMediator.OnUpdate += UpdateTotalMediator_OnUpdate;

            viewModel.Ingredients.RemoveAt(1);

            Assert.True(hasBeenCalled);

            void UpdateTotalMediator_OnUpdate()
            {
                hasBeenCalled = true;
            }
        }
    }
}
