using System;
using Microsoft.Practices.Unity;

namespace Common.InversionOfControl.Unity3
{
    internal class UnityReadOnlyContainer : IDisposableContainer
    {
        private readonly IUnityContainer _container;

        public UnityReadOnlyContainer(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        public bool IsRegistered<T>()
        {
            return _container.IsRegistered<T>();
        }

        public bool IsRegistered<T>(string name)
        {
            return _container.IsRegistered<T>(name);
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
            _container.Dispose();
        }
    }
}