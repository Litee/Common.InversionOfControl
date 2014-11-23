using System;
using Common.InversionOfControl.Tests.HelperClasses;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
{
    public abstract partial class ContainerBuilderTests
    {
        [Test]
        public void ShouldRegisterTypeAsTransientByDefault()
        {
            Target.Register<TestServiceOne>();
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<TestServiceOne>();
            var secondInstance = container.GetInstance<TestServiceOne>();
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterNamedTypeAsTransientByDefault()
        {
            Target.Register<TestServiceOne>("one");
            Target.Register<TestServiceOne>("two");
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<TestServiceOne>("one");
            var secondInstance = container.GetInstance<TestServiceOne>("one");
            var thirdInstance = container.GetInstance<TestServiceOne>("two");
            var fourthInstance = container.GetInstance<TestServiceOne>("two");
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreNotSame(secondInstance, thirdInstance);
            Assert.AreNotSame(thirdInstance, fourthInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
            Assert.AreEqual("I am TestServiceOne", thirdInstance.Call());
            Assert.AreEqual("I am TestServiceOne", fourthInstance.Call());
        }

        [Test]
        public void ShouldRegisterTypeAsTransient()
        {
            Target.Register<TestServiceOne>(Scope.Transient);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<TestServiceOne>();
            var secondInstance = container.GetInstance<TestServiceOne>();
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterNamedTypeAsTransient()
        {
            Target.Register<TestServiceOne>("one", Scope.Transient);
            Target.Register<TestServiceOne>("two", Scope.Transient);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<TestServiceOne>("one");
            var secondInstance = container.GetInstance<TestServiceOne>("one");
            var thirdInstance = container.GetInstance<TestServiceOne>("two");
            var fourthInstance = container.GetInstance<TestServiceOne>("two");
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreNotSame(secondInstance, thirdInstance);
            Assert.AreNotSame(thirdInstance, fourthInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
            Assert.AreEqual("I am TestServiceOne", thirdInstance.Call());
            Assert.AreEqual("I am TestServiceOne", fourthInstance.Call());
        }

        [Test]
        public void ShouldRegisterTypeAsSingleton()
        {
            Target.Register<TestServiceOne>(Scope.Singleton);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<TestServiceOne>();
            var secondInstance = container.GetInstance<TestServiceOne>();
            Assert.AreSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }

        [Test]
        public void ShouldRegisterNamedTypeAsSingleton()
        {
            Target.Register<TestServiceOne>("one", Scope.Singleton);
            Target.Register<TestServiceOne>("two", Scope.Singleton);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<TestServiceOne>("one");
            var secondInstance = container.GetInstance<TestServiceOne>("one");
            var thirdInstance = container.GetInstance<TestServiceOne>("two");
            var fourthInstance = container.GetInstance<TestServiceOne>("two");
            Assert.AreSame(firstInstance, secondInstance);
            Assert.AreNotSame(secondInstance, thirdInstance);
            Assert.AreSame(thirdInstance, fourthInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
            Assert.AreEqual("I am TestServiceOne", thirdInstance.Call());
            Assert.AreEqual("I am TestServiceOne", fourthInstance.Call());
        }

        [Test]
        public void ShouldRegisterNamedTypeOneAsSingletonAndOneAsTransient()
        {
            Target.Register<TestServiceOne>("one", Scope.Transient);
            Target.Register<TestServiceOne>("two", Scope.Singleton);
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<TestServiceOne>("one");
            var secondInstance = container.GetInstance<TestServiceOne>("one");
            var thirdInstance = container.GetInstance<TestServiceOne>("two");
            var fourthInstance = container.GetInstance<TestServiceOne>("two");
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreNotSame(secondInstance, thirdInstance);
            Assert.AreSame(thirdInstance, fourthInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
            Assert.AreEqual("I am TestServiceOne", thirdInstance.Call());
            Assert.AreEqual("I am TestServiceOne", fourthInstance.Call());
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailWhenNameIsNull()
        {
            Target.Register<TestServiceOne>((string) null);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailWhenNameIsNullWithScope()
        {
            Target.Register<TestServiceOne>((string) null, Scope.Singleton);
        }
    }
}