using System;
using System.Linq;
using Common.InversionOfControl.Tests.HelperClasses;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
{
    public abstract partial class ContainerBuilderTests
    {
        [Test]
        public void ShouldResolveMultipleUnnamedImplementations()
        {
            Target.Register<ITestService, TestServiceOne>();
            Target.Register<ITestService, TestServiceTwo>();
            IDisposableContainer container = Target.Build();
            //
            var instances = container.GetAllInstances<ITestService>();
            Assert.AreEqual(2, instances.Count());
            Assert.IsTrue(instances.Any(x => x.Call() == "I am TestServiceOne"));
            Assert.IsTrue(instances.Any(x => x.Call() == "I am TestServiceTwo"));
        }

        [Test]
        public void ShouldResolveMultipleUnnamedImplementationsWitScope()
        {
            Target.Register<ITestService, TestServiceOne>(Scope.Singleton);
            Target.Register<ITestService, TestServiceTwo>(Scope.Singleton);
            IDisposableContainer container = Target.Build();
            //
            var instances = container.GetAllInstances<ITestService>();
            Assert.AreEqual(2, instances.Count());
            Assert.IsTrue(instances.Any(x => x.Call() == "I am TestServiceOne"));
            Assert.IsTrue(instances.Any(x => x.Call() == "I am TestServiceTwo"));
        }

        [Test]
        public void ShouldResolveMultipleNamedImplementations()
        {
            Target.Register<ITestService, TestServiceOne>("one");
            Target.Register<ITestService, TestServiceTwo>("two");
            IDisposableContainer container = Target.Build();
            //
            var instances = container.GetAllInstances<ITestService>();
            Assert.AreEqual(2, instances.Count());
            Assert.IsTrue(instances.Any(x => x.Call() == "I am TestServiceOne"));
            Assert.IsTrue(instances.Any(x => x.Call() == "I am TestServiceTwo"));
        }

        [Test]
        public void ShouldResolveMultipleNamedImplementationsWithScope()
        {
            Target.Register<ITestService, TestServiceOne>("one", Scope.Singleton);
            Target.Register<ITestService, TestServiceTwo>("two", Scope.Singleton);
            IDisposableContainer container = Target.Build();
            //
            var instances = container.GetAllInstances<ITestService>();
            Assert.AreEqual(2, instances.Count());
            Assert.IsTrue(instances.Any(x => x.Call() == "I am TestServiceOne"));
            Assert.IsTrue(instances.Any(x => x.Call() == "I am TestServiceTwo"));
        }
    }
}