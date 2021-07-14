using ERP.Common.Models;

namespace ERP.Models.Margin
{
    public class Ingredient : BaseModel
    {
        public string Name { get => Get(); set => Set(value); }

        public double Price { get => Get(); set => Set(value); }
    }
}
