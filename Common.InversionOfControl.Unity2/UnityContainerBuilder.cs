using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Common.InversionOfControl.Unity2
{
    public class UnityContainerBuilder : IContainerBuilder
    {
        private readonly UnityContainer _container;

        public UnityContainerBuilder()
        {
            _container = new UnityContainer();
            _container.AddExtension(new MyExtension());
        }

        public IDisposableContainer Build()
        {
            return new UnityReadOnlyContainer(_container);
        }

        public IContainerBuilder RegisterSingleton<T>(T instance) where T : class
        {
            _container.RegisterInstance(instance);
            return this;
        }

        public IContainerBuilder RegisterSingleton<T>(T instance, string name) where T : class
        {
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
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(string name) where TInterface : class where TImplementation : class, TInterface
        {
            _container.RegisterType<TInterface, TImplementation>(name, new TransientLifetimeManager());
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
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(string name, Scope scope) where TInterface : class where TImplementation : class, TInterface
        {
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
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory) where T : class
        {
            _container.RegisterType<T>(new TransientLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container))));
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, string name) where T : class
        {
            _container.RegisterType<T>(name, new TransientLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container))));
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _container.RegisterType<T>(new ContainerControlledLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container))));
                    break;
                case Scope.Transient:
                    _container.RegisterType<T>(new TransientLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container))));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, string name, Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _container.RegisterType<T>(name, new ContainerControlledLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container))));
                    break;
                case Scope.Transient:
                    _container.RegisterType<T>(name, new TransientLifetimeManager(), new InjectionFactory(container => factory(new UnityReadOnlyContainer(container))));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }
    }

    public class MyExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.Add(new MyStrategy(), UnityBuildStage.PreCreation);
        }
    }
}