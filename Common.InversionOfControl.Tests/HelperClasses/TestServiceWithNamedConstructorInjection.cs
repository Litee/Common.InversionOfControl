using System;

namespace Common.InversionOfControl.Tests.HelperClasses
{
    public class TestServiceWithNamedConstructorInjection : IAnotherTestService
    {
        private readonly ITestService _testService1;
        private readonly ITestService _testService2;

        public TestServiceWithNamedConstructorInjection([NamedDependency("service1")] ITestService testServiceOne, [NamedDependency("service2")] ITestService testServiceTwo)
        {
            if (testServiceOne == null) throw new ArgumentNullException("testServiceOne");
            if (testServiceTwo == null) throw new ArgumentNullException("testServiceTwo");
            _testService1 = testServiceOne;
            _testService2 = testServiceTwo;
        }

        public string CallChildService1()
        {
            return _testService1.Call();
        }

        public string CallChildService2()
        {
            return _testService2.Call();
        }
    }
}