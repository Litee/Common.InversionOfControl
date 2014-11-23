using Common.InversionOfControl.Tests;
using NUnit.Framework;

namespace Common.InversionOfControl.Autofac.Tests
{
    [TestFixture]
    public class AutofacContainerBuilderTests : ContainerBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            Target = new AutofacContainerBuilder();
        }
    }
}