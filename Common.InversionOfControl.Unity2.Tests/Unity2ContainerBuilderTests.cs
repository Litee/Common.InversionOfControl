using Common.InversionOfControl.Tests.HelperClasses;
using Common.InversionOfControl.Unity2;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
{
    [TestFixture]
    [Ignore("For some reason backport to Unity 2.1 stopped working")]
    public class Unity2ContainerBuilderTests : ContainerBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            Target = new UnityContainerBuilder();
        }

        [Test]
        public void ShouldResolveConstructorInjection2()
        {
//            IDisposableContainer container2 = Target.Build();
            Target.Register<IAnotherTestService, TestServiceWithConstructorInjection>();
            Target.Register<ITestService, TestServiceOne>();
//            container.RegisterType<IAnotherTestService, TestServiceWithConstructorInjection>(new TransientLifetimeManager());
//            container.RegisterType<ITestService, TestServiceOne>(new TransientLifetimeManager());
            IDisposableContainer container = Target.Build();
            //
            var firstInstance = container.GetInstance<IAnotherTestService>();
            var secondInstance = container.GetInstance<IAnotherTestService>();
//            var firstInstance = container.Resolve<IAnotherTestService>();
//            var secondInstance = container.Resolve<IAnotherTestService>();
            Assert.AreNotSame(firstInstance, secondInstance);
            Assert.AreEqual("I am TestServiceOne", firstInstance.Call());
            Assert.AreEqual("I am TestServiceOne", secondInstance.Call());
        }
    }
}