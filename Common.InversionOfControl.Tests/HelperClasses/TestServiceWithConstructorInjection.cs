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

        public string Call()
        {
            return _testService.Call();
        }
    }
}