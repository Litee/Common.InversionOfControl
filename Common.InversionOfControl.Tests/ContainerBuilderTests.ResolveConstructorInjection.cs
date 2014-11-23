using Common.InversionOfControl.Tests.HelperClasses;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
{
    public abstract partial class ContainerBuilderTests
    {
        [Test]
        public void ShouldResolveConstructorInjection()
        {
            Target.Register<IAnotherTestService, TestServiceWithConstructorInjection>();
            Target.Register<ITestService, TestServiceOne>();
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<IAnotherTestService>();
            var secondInstance = container.GetInstance<IAnotherTestService>();
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldResolveNamedConstructorInjection()
        {
            Target.Register<IAnotherTestService, TestServiceWithNamedConstructorInjection>();
            Target.Register<ITestService, TestServiceOne>("service1");
            Target.Register<ITestService, TestServiceTwo>("service2");
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<IAnotherTestService>();
            var secondInstance = container.GetInstance<IAnotherTestService>();
            Assert.IsNotNull(firstInstance);
            Assert.IsNotNull(secondInstance);
            Assert.AreNotSame(firstInstance, secondInstance);
        }
    }
}