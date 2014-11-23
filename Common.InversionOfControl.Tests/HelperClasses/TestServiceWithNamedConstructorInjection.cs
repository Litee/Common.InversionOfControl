using System;

namespace Common.InversionOfControl.Tests.HelperClasses
{
    public class TestServiceWithNamedConstructorInjection : IAnotherTestService
    {
        private readonly ITestService _testService;

        public TestServiceWithNamedConstructorInjection([NamedDependency("service1")] ITestService testService)
        {
            if (testService == null) throw new ArgumentNullException("testService");
            _testService = testService;
        }
    }
}