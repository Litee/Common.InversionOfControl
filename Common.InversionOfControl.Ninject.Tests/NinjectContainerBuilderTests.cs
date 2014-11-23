using Common.InversionOfControl.Tests;
using NUnit.Framework;

namespace Common.InversionOfControl.Ninject.Tests
{
    [TestFixture]
    public class NinjectContainerBuilderTests : ContainerBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            Target = new NinjectContainerBuilder();
        }
    }
}