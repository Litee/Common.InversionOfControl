using Common.InversionOfControl.Tests.HelperClasses;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
{
    public abstract partial class ContainerBuilderTests
    {
        [Test]
        public void ShouldRegisterImplementationTypeAsTransientByDefault()
        {
            Target.Register<ITestService, TestServiceOne>();
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<ITestService>();
            var secondInstance = container.GetInstance<ITestService>();
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterNamedImplementationTypeAsTransientByDefault()
        {
            Target.Register<ITestService, TestServiceOne>("one");
            Target.Register<ITestService, TestServiceOne>("two");
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<ITestService>("one");
            var secondInstance = container.GetInstance<ITestService>("one");
            var thirdInstance = container.GetInstance<ITestService>("two");
            var fourthInstance = container.GetInstance<ITestService>("two");
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreNotSame(secondInstance, thirdInstance);
            Assert.AreNotSame(thirdInstance, fourthInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
            Assert.AreEqual("I am TestServiceOne", thirdInstance.Call());
            Assert.AreEqual("I am TestServiceOne", fourthInstance.Call());
        }

        [Test]
        public void ShouldRegisterImplementationTypeAsTransient()
        {
            Target.Register<ITestService, TestServiceOne>(Scope.Transient);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<ITestService>();
            var secondInstance = container.GetInstance<ITestService>();
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterNamedImplementationTypeAsTransient()
        {
            Target.Register<ITestService, TestServiceOne>("one", Scope.Transient);
            Target.Register<ITestService, TestServiceOne>("two", Scope.Transient);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<ITestService>("one");
            var secondInstance = container.GetInstance<ITestService>("one");
            var thirdInstance = container.GetInstance<ITestService>("two");
            var fourthInstance = container.GetInstance<ITestService>("two");
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreNotSame(secondInstance, thirdInstance);
            Assert.AreNotSame(thirdInstance, fourthInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
            Assert.AreEqual("I am TestServiceOne", thirdInstance.Call());
            Assert.AreEqual("I am TestServiceOne", fourthInstance.Call());
        }

        [Test]
        public void ShouldRegisterImplementationTypeAsSingleton()
        {
            Target.Register<ITestService, TestServiceOne>(Scope.Singleton);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<ITestService>();
            var secondInstance = container.GetInstance<ITestService>();
            Assert.AreSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterImplementationTypeAsNamedSingleton()
        {
            Target.Register<ITestService, TestServiceOne>("one", Scope.Singleton);
            Target.Register<ITestService, TestServiceOne>("two", Scope.Singleton);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<ITestService>("one");
            var secondInstance = container.GetInstance<ITestService>("one");
            var thirdInstance = container.GetInstance<ITestService>("two");
            var fourthInstance = container.GetInstance<ITestService>("two");
            Assert.AreSame(firstInstance, secondInstance);
            Assert.AreNotSame(secondInstance, thirdInstance);
            Assert.AreSame(thirdInstance, fourthInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
            Assert.AreEqual("I am TestServiceOne", thirdInstance.Call());
            Assert.AreEqual("I am TestServiceOne", fourthInstance.Call());
        }
    }
}