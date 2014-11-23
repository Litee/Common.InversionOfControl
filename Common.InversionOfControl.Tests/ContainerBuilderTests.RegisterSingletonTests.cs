using Common.InversionOfControl.Tests.HelperClasses;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
{
    public abstract partial class ContainerBuilderTests
    {
        [Test]
        public void ShouldRegisterInstanceAsSingleton()
        {
            Target.RegisterSingleton<ITestService>(new TestServiceOne());
            IDisposableContainer container = Target.Build();
            //
            Assert.IsTrue(container.IsRegistered<ITestService>());
            Assert.IsFalse(container.IsRegistered<TestServiceTwo>());
            var firstInstance = container.GetInstance<ITestService>();
            var secondInstance = container.GetInstance<ITestService>();
            Assert.AreSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterInstanceAsNamedSingleton()
        {
            Target.RegisterSingleton<ITestService>(new TestServiceOne(), "one");
            Target.RegisterSingleton<ITestService>(new TestServiceTwo(), "two");
            IDisposableContainer container = Target.Build();
            //
            Assert.IsTrue(container.IsRegistered<ITestService>("one"));
            Assert.IsTrue(container.IsRegistered<ITestService>("two"));
            Assert.IsFalse(container.IsRegistered<ITestService>("three"));
            var firstInstance = container.GetInstance<ITestService>("one");
            var secondInstance = container.GetInstance<ITestService>("one");
            var thirdInstance = container.GetInstance<ITestService>("two");
            Assert.AreSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
            Assert.AreEqual("I am TestServiceTwo", thirdInstance.Call());
        }
    }
}