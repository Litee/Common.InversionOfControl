using Common.InversionOfControl.Autofac;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
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