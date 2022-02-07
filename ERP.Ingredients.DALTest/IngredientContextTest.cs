using ERP.Common.Helpers.ConnectionString;
using ERP.Ingredients.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using Unity;

namespace ERP.Ingredients.DALTest
{
    [TestFixture]
    internal class IngredientContextTest
    {
        private const string CipheredFileName = "cipheredlocaldatabase.txt";

        [OneTimeSetUp]
        public void CipherConnectionString()
        {
            UnityContainer container = new();
            container.RegisterConnectionStringEncryption();

            string plainConnectionString = File.ReadAllText("localdatabase.txt");
            IEncryptor encryptor = container.Resolve<IEncryptor>();
            string cipheredText = encryptor.Encrypt(plainConnectionString);
            File.WriteAllText(CipheredFileName, cipheredText);
        }

        [OneTimeSetUp]
        public void MigrateDatabase()
        {
            IngredientContext context = BuildContextWithCipheredConnectionStringFile();
            context.Database.Migrate();
        }

        private IngredientContext BuildContextWithCipheredConnectionStringFile()
        {
            UnityContainer container = new();
            container.RegisterType<IConnectionStringResolver, EncryptedFileConnectionStringResolver>(new Unity.Injection.InjectionConstructor(CipheredFileName));
            
            IngredientContext context = new(container);

            return context;
        }

        [Test]
        public void LocalDatabaseForUnitTests()
        {
            IngredientContext context = BuildContextWithCipheredConnectionStringFile();
            _ = context.Ingredients.ToList();
        }

        [Test]
        public void CreateAndRead()
        {
            string expected, actual;
            int id;

            IngredientDTO ingredient = new()
            {
                Name = Guid.NewGuid().ToString(),
                Price = 22.03
            };

            expected = ingredient.Name;

            using (IngredientContext context = BuildContextWithCipheredConnectionStringFile())
            {
                context.Ingredients.Add(ingredient);
                context.SaveChanges();
                id = ingredient.Id;
            }

            using (IngredientContext context = BuildContextWithCipheredConnectionStringFile())
            {
                IngredientDTO? found = context.Ingredients.Find(id);
                Assert.NotNull(found);
                actual = found?.Name ?? throw new ArgumentNullException();
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update()
        {
            int id;

            IngredientDTO ingredient = new()
            {
                Name = Guid.NewGuid().ToString(),
                Price = 22.03
            };

            string unexpected = ingredient.Name,
                expected, actual;

            using (IngredientContext context = BuildContextWithCipheredConnectionStringFile())
            {
                context.Ingredients.Add(ingredient);
                context.SaveChanges();
                id = ingredient.Id;
            }

            using (IngredientContext context = BuildContextWithCipheredConnectionStringFile())
            {
                IngredientDTO? found = context.Ingredients.Find(id);
                Assert.NotNull(found);
                
                if (found is not null)
                    found.Name = Guid.NewGuid().ToString();
                
                expected = found?.Name ?? throw new ArgumentNullException();

                context.SaveChanges();
            }

            using (IngredientContext context = BuildContextWithCipheredConnectionStringFile())
            {
                IngredientDTO? found = context.Ingredients.Find(id);
                Assert.NotNull(found);

                actual = found?.Name ?? throw new ArgumentNullException();
            }

            Assert.AreNotEqual(unexpected, actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Delete()
        {
            int id;

            IngredientDTO ingredient = new()
            {
                Name = Guid.NewGuid().ToString(),
                Price = 22.03
            };

            using (IngredientContext context = BuildContextWithCipheredConnectionStringFile())
            {
                context.Ingredients.Add(ingredient);
                context.SaveChanges();
                id = ingredient.Id;
            }

            using (IngredientContext context = BuildContextWithCipheredConnectionStringFile())
            {
                IngredientDTO? found = context.Ingredients.Find(id);
                Assert.NotNull(found);

                if (found is not null)
                    context.Ingredients.Remove(found);

                context.SaveChanges();
            }

            using (IngredientContext context = BuildContextWithCipheredConnectionStringFile())
            {
                IngredientDTO? found = context.Ingredients.Find(id);
                Assert.Null(found);
            }
        }
    }
}
