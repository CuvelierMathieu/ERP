using ERP.Common.Helpers;
using ERP.Common.Models;
using NUnit.Framework;
using System;

namespace ERP.CommonTest.Helpers
{
    [TestFixture]
    public class PropertyValueManagerTest
    {
        [Test]
        public void CanBeInitialized()
        {
            Assert.DoesNotThrow(() => new PropertyValueManager<ModelForPropertyValueManagerTest>());
        }

        [Test]
        public void ReturnsDefaultValueForUnsetProperties()
        {
            PropertyValueManager<ModelForPropertyValueManagerTest> manager = new();

            int expectedId = default;
            DateTime expectedCreationDate = default;

            Assert.AreEqual(expectedId, manager.Get(nameof(ModelForPropertyValueManagerTest.Id)));
            Assert.AreEqual(expectedCreationDate, manager.Get(nameof(ModelForPropertyValueManagerTest.CreationDate)));
        }

        [Test]
        public void SettedValuesCanBeRetrieved()
        {
            PropertyValueManager<ModelForPropertyValueManagerTest> manager = new();

            int expected = -74;

            manager.Set(expected, nameof(ModelForPropertyValueManagerTest.Id));

            int actual = manager.Get(nameof(ModelForPropertyValueManagerTest.Id));

            Assert.AreEqual(expected, actual);
        }
    }

    public class ModelForPropertyValueManagerTest : BaseModel
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime? UpdateDate { get; set; }
    }
}
