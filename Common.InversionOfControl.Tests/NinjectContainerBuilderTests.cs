using Common.InversionOfControl.Ninject;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
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