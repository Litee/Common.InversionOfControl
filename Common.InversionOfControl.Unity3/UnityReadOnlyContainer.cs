using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Common.InversionOfControl.Unity3
{
    internal class UnityReadOnlyContainer : IDisposableContainer
    {
        private readonly IUnityContainer _container;
        private readonly Dictionary<Type, List<Type>> _contractToImplementationMapping;

        public UnityReadOnlyContainer(IUnityContainer container, Dictionary<Type, List<Type>> contractToImplementationMapping)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
            _contractToImplementationMapping = contractToImplementationMapping;
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

        public IEnumerable<T> GetAllInstances<T>()
        {
            return _contractToImplementationMapping[typeof(T)].Select(x => _container.Resolve(x)).Cast<T>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}