using ERP.UI.Common.Mediators;
using ERP.UI.Margin.IoC;
using ERP.UI.Margin.Mediators;
using NUnit.Framework;
using System;
using System.Linq;
using Unity;

namespace ERP.UI.MarginTest.IoC
{
    [TestFixture]
    public class MarginContainerTest
    {
        public static object[] InterfacesAndTypesToBeTested() => new object[]
        {
            new object[2] { typeof(IBindingUpdateMediator), typeof(UpdateMediator) },
        };

        public static object[] OnlyInterfacesToBeTested()
        {
            return InterfacesAndTypesToBeTested()
                .Cast<object[]>()
                .Select(x => x[0])
                .ToArray();
        }

        [Test]
        public void ContainerIsInitialized()
        {
            MarginContainer container = MarginContainer.Instance;
            Assert.IsNotNull(container);
        }

        [Test]
        public void TryingToResolveUnregisteredInterfaceThrows()
        {
            MarginContainer container = MarginContainer.Instance;
            Assert.Throws<ResolutionFailedException>(() => container.Resolve<IUnregisteredInterface>());
        }

        [Test]
        public void ContainerIsASingleton()
        {
            MarginContainer firstCall = MarginContainer.Instance;
            MarginContainer secondCall = MarginContainer.Instance;

            int expected = firstCall.GetHashCode();
            int actual = secondCall.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        
        [Test]
        [TestCaseSource(nameof(OnlyInterfacesToBeTested))]
        public void CanResolve(Type type)
        {
            Assert.DoesNotThrow(() => _ = MarginContainer.Instance.Resolve(type));
        }

        [Test]
        [TestCaseSource(nameof(OnlyInterfacesToBeTested))]
        public void ResolvedObjectIsNotNull(Type type)
        {
            object? mediator = MarginContainer.Instance.Resolve(type);

            Assert.NotNull(mediator);
        }

        [Test]
        [TestCaseSource(nameof(InterfacesAndTypesToBeTested))]
        public void ResolvedObjectIsAnInstanceOfDesiredClass(Type interfaceType, Type classType)
        {
            object? resolvedObject = MarginContainer.Instance.Resolve(interfaceType);

            Assert.AreEqual(resolvedObject.GetType(), classType);
        }

        private interface IUnregisteredInterface
        { }
    }
}
