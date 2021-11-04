using ERP.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models.Margin
{
    public class Recipe : BaseModel
    {
        public int Id { get => Get(); set => Set(value); }

        public string Name { get => Get(); set => Set(value); }

        public ObservableCollection<Ingredient> Ingredients { get => Get(); set => Set(value); }
    }
}
