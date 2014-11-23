using System;
using Castle.MicroKernel;

namespace Common.InversionOfControl.CastleWindsor
{
    internal class WindsorReadOnlyContainer : IDisposableContainer
    {
        private readonly IKernel _container;

        public WindsorReadOnlyContainer(IKernel container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        public bool IsRegistered<T>()
        {
            return _container.HasComponent(typeof(T));
        }

        public bool IsRegistered<T>(string name)
        {
            return _container.HasComponent(name);
        }

        public T GetInstance<T>()
        {
            return _container.Resolve<T>();
        }

        public T GetInstance<T>(string name)
        {
            return _container.Resolve<T>(name);
        }

        public void Dispose()
        {
            // TODO
        }
    }
}