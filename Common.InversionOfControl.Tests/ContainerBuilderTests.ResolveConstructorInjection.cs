using Common.InversionOfControl.Tests.HelperClasses;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
{
    public abstract partial class ContainerBuilderTests
    {
        [Test]
        public void ShouldResolveConstructorInjection()
        {
            Target.Register<TestServiceWithConstructorInjection>();
            Target.Register<ITestService>(c => new TestServiceOne());
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<TestServiceWithConstructorInjection>();
            var secondInstance = container.GetInstance<TestServiceWithConstructorInjection>();
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldResolveNamedConstructorInjection()
        {
            Target.Register<IAnotherTestService, TestServiceWithNamedConstructorInjection>();
            Target.RegisterSingleton<ITestService>(new TestServiceOne(), "service1");
            Target.RegisterSingleton<ITestService>(new TestServiceTwo(), "service2");
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