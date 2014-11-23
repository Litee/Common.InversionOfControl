using System;

namespace Common.InversionOfControl.Tests.HelperClasses
{
    public class TestServiceWithConstructorInjection : IAnotherTestService
    {
        private readonly ITestService _testService;

        public TestServiceWithConstructorInjection(ITestService testService)
        {
            if (testService == null) throw new ArgumentNullException("testService");
            _testService = testService;
        }

        public string CallChildService1()
        {
            return _testService.Call();
        }

        public string CallChildService2()
        {
            throw new NotImplementedException();
        }
    }
}