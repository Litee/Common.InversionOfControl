namespace Common.InversionOfControl.Tests.HelperClasses
{
    public class TestServiceOne : ITestService
    {
        public string Call()
        {
            return "I am TestServiceOne";
        }
    }
}