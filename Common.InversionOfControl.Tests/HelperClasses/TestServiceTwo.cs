namespace Common.InversionOfControl.Tests.HelperClasses
{
    public class TestServiceTwo : ITestService
    {
        public string Call()
        {
            return "I am TestServiceTwo";
        }
    }
}