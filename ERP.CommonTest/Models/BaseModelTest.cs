using ERP.Common.Models;
using NUnit.Framework;
using System;

namespace ERP.CommonTest.Models
{
    [TestFixture]
    public class BaseModelTest
    {
        [Test]
        public void CanBeInitialized()
        {
            Assert.DoesNotThrow(() => new ModelDirectlyInheritedFromBaseModel());
        }

        [Test]
        public void SettedValueCanBeRetrieved()
        {
            ModelForBaseModelTest model = new();

            string expected = Guid.NewGuid().ToString();

            model.Name = expected;

            string actual = model.Name;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TwoObjectsOfSameTypeDontShareValues()
        {
            ModelForBaseModelTest firstModel = new(),
                secondModel = new();

            string firstModelName = Guid.NewGuid().ToString();
            string secondModelName = Guid.NewGuid().ToString();

            firstModel.Name = firstModelName;
            secondModel.Name = secondModelName;

            Assert.AreNotEqual(firstModel.Name, secondModelName);
            Assert.AreNotEqual(firstModel.Name, secondModel.Name);
        }

        [Test]
        public void SetAValueCallsPropertyChanged()
        {
            ModelForBaseModelTest model = new();
            bool hasBeenCalled = false;

            model.PropertyChanged += Model_PropertyChanged;

            model.Name = Guid.NewGuid().ToString();

            Assert.True(hasBeenCalled);

            void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                hasBeenCalled = true;
            }
        }

        [Test]
        public void SetAValueToNullCallsPropertyChanged()
        {
            ModelForBaseModelTest model = new();
            bool hasBeenCalled = false;
            model.Name = Guid.NewGuid().ToString();

            model.PropertyChanged += Model_PropertyChanged;

            model.Name = null;

            Assert.True(hasBeenCalled);

            void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                hasBeenCalled = true;
            }
        }

        [Test]
        [TestCase("old")]
        [TestCase(null)]
        public void SetAValueDoesntCallPropertyChangedIfNewValueAndOldOneAreEquals(string value)
        {
            ModelForBaseModelTest model = new();
            bool hasBeenCalled = false;
            
            string name = Guid.NewGuid().ToString();
            model.Name = name;

            model.PropertyChanged += Model_PropertyChanged;
            model.Name = name;

            Assert.False(hasBeenCalled);

            void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                hasBeenCalled = true;
            }
        }
    }

    public class ModelDirectlyInheritedFromBaseModel : BaseModel
    { }

    public class ModelForBaseModelTest : BaseModel
    {
        public string Name { get => Get(); set => Set(value); }
    }
}
