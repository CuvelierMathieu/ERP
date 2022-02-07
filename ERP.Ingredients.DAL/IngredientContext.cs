using ERP.Common.Helpers.ConnectionString;
using Microsoft.EntityFrameworkCore;
using Unity;

namespace ERP.Ingredients.DAL
{
    public class IngredientContext : DbContext
    {
        public IUnityContainer container;

        public DbSet<IngredientDTO> Ingredients { get; set; }

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public IngredientContext() : base()
        { }
        
        public IngredientContext(IUnityContainer container)
        {
            this.container = container;
        }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString;

            if (container is not null)
                using (IConnectionStringResolver connectionStringResolver = container.Resolve<IConnectionStringResolver>())
                    connectionString = connectionStringResolver.GetConnectionString();
            else
                connectionString = Common.DAL.DefaultConnectionString.Value;

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
