using Common.InversionOfControl.Tests;
using NUnit.Framework;

namespace Common.InversionOfControl.Unity3.Tests
{
    [TestFixture]
    public class Unity3ContainerBuilderTests : ContainerBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            Target = new UnityContainerBuilder();
        }
    }
}