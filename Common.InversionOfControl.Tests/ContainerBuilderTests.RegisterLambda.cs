using Common.InversionOfControl.Tests.HelperClasses;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
{
    public abstract partial class ContainerBuilderTests
    {
        [Test]
        public void ShouldRegisterLambdaAsTransientByDefault()
        {
            Target.Register<ITestService>(c => new TestServiceOne());
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<ITestService>();
            var secondInstance = container.GetInstance<ITestService>();
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterNamedLambdaAsTransientByDefault()
        {
            Target.Register<ITestService>(c => new TestServiceOne(), "one");
            Target.Register<ITestService>(c => new TestServiceOne(), "two");
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
        public void ShouldRegisterLambdaAsTransient()
        {
            Target.Register<ITestService>(c => new TestServiceOne(), Scope.Transient);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<ITestService>();
            var secondInstance = container.GetInstance<ITestService>();
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterNamedLambdaAsTransient()
        {
            Target.Register<ITestService>(c => new TestServiceOne(), "one", Scope.Transient);
            Target.Register<ITestService>(c => new TestServiceOne(), "two", Scope.Transient);
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
        public void ShouldRegisterLambdaAsSingleton()
        {
            Target.Register<ITestService>(c => new TestServiceOne(), Scope.Singleton);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<ITestService>();
            var secondInstance = container.GetInstance<ITestService>();
            Assert.AreSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterNamedLambdaAsSingleton()
        {
            Target.Register<ITestService>(c => new TestServiceOne(), "one", Scope.Singleton);
            Target.Register<ITestService>(c => new TestServiceOne(), "two", Scope.Singleton);
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

        [Test]
        public void ShouldRegisterLambdaWithConstructorInjection()
        {
            Target.Register<IAnotherTestService>(c => new TestServiceWithConstructorInjection(c.GetInstance<ITestService>()));
            Target.Register<ITestService>(c => new TestServiceOne());
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