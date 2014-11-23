using Common.InversionOfControl.Unity;
using NUnit.Framework;

namespace Common.InversionOfControl.Tests
{
    [TestFixture]
    public class UnityContainerBuilderTests : ContainerBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            Target = new UnityContainerBuilder();
        }
    }
}