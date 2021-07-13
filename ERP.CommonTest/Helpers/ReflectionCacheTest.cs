using ERP.Common.Helpers;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace ERP.CommonTest.Helpers
{
    [TestFixture]
    public class ReflectionCacheTest
    {
        public int PropertyToBeRetrievedInTests { get; set; }

        [Test]
        public void CanGetProperties()
        {
            PropertyInfo[] propertyInfos = null;

            Assert.DoesNotThrow(() => propertyInfos = ReflectionCache.GetPropertiesForType(typeof(ReflectionCacheTest)));
            Assert.IsNotNull(propertyInfos);

            PropertyInfo property = propertyInfos.Single(p => p.Name == nameof(PropertyToBeRetrievedInTests));
        }
    }
}
