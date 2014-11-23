namespace Common.InversionOfControl
{
    public interface IContainer
    {
        bool IsRegistered<T>();
        bool IsRegistered<T>(string name);

        T GetInstance<T>();
        T GetInstance<T>(string name);
    }
}