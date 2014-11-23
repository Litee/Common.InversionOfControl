using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;

namespace Common.InversionOfControl.Autofac
{
    internal class AutofacReadOnlyContainer : IDisposableContainer
    {
        private readonly IComponentContext _container;

        public AutofacReadOnlyContainer(IComponentContext container)
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
            return _container.IsRegisteredWithName<T>(name);
        }

        public T GetInstance<T>()
        {
            return _container.Resolve<T>();
        }

        public T GetInstance<T>(string name)
        {
            return _container.ResolveNamed<T>(name);
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            return _container.Resolve<IEnumerable<T>>();
        }

        public void Dispose()
        {
            // TODO
        }
    }
}