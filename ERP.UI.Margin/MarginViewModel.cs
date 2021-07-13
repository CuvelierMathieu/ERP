using ERP.Models.Margin;
using System.Collections.ObjectModel;

namespace ERP.UI.Margin
{
    public class MarginViewModel
    {
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        public MarginViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            Ingredients = new()
            {
                new() { Name = "Sel", Price = 1.2 },
                new() { Name = "Poivre", Price = 0.34 },
            };
        }
    }
}
