using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Common.InversionOfControl.Unity3
{
    public class UnityContainerBuilder : IContainerBuilder
    {
        private readonly UnityContainer _container;
        private readonly Dictionary<Type, List<Type>> _contractToImplementationMapping = new Dictionary<Type, List<Type>>();

        public UnityContainerBuilder()
        {
            _container = new UnityContainer();
            _container.AddExtension(new UnityContainerBuilderExtension());
//            _container.AddNewExtension<CompositionIntegration>();
        }

        public IDisposableContainer Build()
        {
            return new UnityReadOnlyContainer(_container, _contractToImplementationMapping);
        }

        public IContainerBuilder RegisterSingleton<T>(T instance) where T : class
        {
            _container.RegisterInstance(instance);
            return this;
        }

        public IContainerBuilder RegisterSingleton<T>(T instance, string name) where T : class
        {
            if (name == null) throw new ArgumentNullException("name");
            _container.RegisterInstance(name, instance);
            return this;
        }

        public IContainerBuilder Register<T>() where T : class
        {
            _container.RegisterType<T>(new TransientLifetimeManager());
            return this;
        }

        public IContainerBuilder Register<T>(string name) where T : class
        {
            if (name == null) throw new ArgumentNullException("name");
            _container.RegisterType<T>(name, new TransientLifetimeManager());
            return this;
        }

        public IContainerBuilder Register<T>(Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _container.RegisterType<T>(new ContainerControlledLifetimeManager());
                    break;
                case Scope.Transient:
                    _container.RegisterType<T>(new TransientLifetimeManager());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<T>(string name, Scope scope) where T : class
        {
            if (name == null) throw new ArgumentNullException("name");
            switch (scope)
            {
                case Scope.Singleton:
                    _container.RegisterType<T>(name, new ContainerControlledLifetimeManager());
                    break;
                case Scope.Transient:
                    _container.RegisterType<T>(name, new TransientLifetimeManager());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            _container.RegisterType<TInterface, TImplementation>(new TransientLifetimeManager());
            RegisterContractAndImplementation(typeof (TInterface), typeof (TImplementation));
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(string name) where TInterface : class where TImplementation : class, TInterface
        {
            if (name == null) throw new ArgumentNullException("name");
            _container.RegisterType<TInterface, TImplementation>(name, new TransientLifetimeManager());
            RegisterContractAndImplementation(typeof(TInterface), typeof(TImplementation));
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(Scope scope) where TInterface : class where TImplementation : class, TInterface
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _container.RegisterType<TInterface, TImplementation>(new ContainerControlledLifetimeManager());
                    break;
                case Scope.Transient:
                    _container.RegisterType<TInterface, TImplementation>(new TransientLifetimeManager());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            RegisterContractAndImplementation(typeof(TInterface), typeof(TImplementation));
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(string name, Scope scope) where TInterface : class where TImplementation : class, TInterface
        {
            if (name == null) throw new ArgumentNullException("name");
            switch (scope)
            {
                case Scope.Singleton:
                    _container.RegisterType<TInterface, TImplementation>(name, new ContainerControlledLifetimeManager());
                    break;
                case Scope.Transient:
                    _container.RegisterType<TInterface, TImplementation>(name, new TransientLifetimeManager());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            RegisterContractAndImplementation(typeof(TInterface), typeof(TImplementation));
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory) where T : class
        {
            _container.RegisterType<T>(new TransientLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container, _contractToImplementationMapping))));
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, string name) where T : class
        {
            if (name == null) throw new ArgumentNullException("name");
            _container.RegisterType<T>(name, new TransientLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container, _contractToImplementationMapping))));
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _container.RegisterType<T>(new ContainerControlledLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container, _contractToImplementationMapping))));
                    break;
                case Scope.Transient:
                    _container.RegisterType<T>(new TransientLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container, _contractToImplementationMapping))));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, string name, Scope scope) where T : class
        {
            if (name == null) throw new ArgumentNullException("name");
            switch (scope)
            {
                case Scope.Singleton:
                    _container.RegisterType<T>(name, new ContainerControlledLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container, _contractToImplementationMapping))));
                    break;
                case Scope.Transient:
                    _container.RegisterType<T>(name, new TransientLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container, _contractToImplementationMapping))));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        private void RegisterContractAndImplementation(Type contractType, Type implementationType)
        {
            List<Type> list;
            if (!_contractToImplementationMapping.TryGetValue(contractType, out list))
            {
                list = new List<Type>();
                _contractToImplementationMapping.Add(contractType, list);
            }
            list.Add(implementationType);
        }
    }
}