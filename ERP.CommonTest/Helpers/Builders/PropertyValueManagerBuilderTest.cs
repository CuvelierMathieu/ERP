using ERP.Common.Helpers;
using ERP.Common.Helpers.Builders;
using ERP.Common.Models;
using NUnit.Framework;
using System;

namespace ERP.CommonTest.Helpers.Builders
{
    [TestFixture]
    public class PropertyValueManagerBuilderTest
    {
        [Test]
        public void BuildReturnsExpectedManager()
        {
            IPropertyValueManager manager = PropertyValueManagerBuilder.BuildForType(typeof(ModelForPropertyValueManagerBuilderTest));

            Assert.IsTrue(manager is PropertyValueManager<ModelForPropertyValueManagerBuilderTest>);
        }
    }

    public class ModelForPropertyValueManagerBuilderTest : BaseModel
    {
        public Guid Id { get; set; }
    }
}
