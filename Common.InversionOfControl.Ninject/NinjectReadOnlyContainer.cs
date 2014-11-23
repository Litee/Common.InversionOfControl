using Ninject;

namespace Common.InversionOfControl.Ninject
{
    internal class NinjectReadOnlyContainer : IDisposableContainer
    {
        private readonly IKernel _kernel;

        public NinjectReadOnlyContainer(IKernel kernel)
        {
            _kernel = kernel;
        }

        public bool IsRegistered<T>()
        {
            return _kernel.CanResolve<T>();
        }

        public bool IsRegistered<T>(string name)
        {
            return _kernel.CanResolve<T>(name);
        }

        public T GetInstance<T>()
        {
            return _kernel.Get<T>();
        }

        public T GetInstance<T>(string name)
        {
            return _kernel.Get<T>(name);
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }
    }
}