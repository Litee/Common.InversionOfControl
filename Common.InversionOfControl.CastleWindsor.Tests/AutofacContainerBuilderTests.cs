using Common.InversionOfControl.Tests;
using NUnit.Framework;

namespace Common.InversionOfControl.CastleWindsor.Tests
{
    [TestFixture]
    public class CastleWindsorContainerBuilderTests : ContainerBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            Target = new WindsorContainerBuilder();
        }
    }
}