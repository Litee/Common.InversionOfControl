using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Common.InversionOfControl.CastleWindsor
{
    public class WindsorContainerBuilder : IContainerBuilder
    {
        private readonly WindsorContainer _container;

        public WindsorContainerBuilder()
        {
            _container = new WindsorContainer();
        }

        public IDisposableContainer Build()
        {
            return new WindsorReadOnlyContainer(_container.Kernel);
        }

        public IContainerBuilder RegisterSingleton<T>(T instance) where T : class
        {
            _container.Register(Component.For<T>().Instance(instance).LifestyleSingleton());
            return this;
        }

        public IContainerBuilder RegisterSingleton<T>(T instance, string name) where T : class
        {
            _container.Register(Component.For<T>().Instance(instance).LifestyleSingleton().Named(name));
            return this;
        }

        public IContainerBuilder Register<T>() where T : class
        {
            _container.Register(Component.For<T>().ImplementedBy<T>().LifestyleTransient().AddNamedConstructorInjectionSupport<T, T>());
            return this;
        }

        public IContainerBuilder Register<T>(string name) where T : class
        {
            _container.Register(Component.For<T>().ImplementedBy<T>().LifestyleTransient().Named(name).AddNamedConstructorInjectionSupport<T, T>());
            return this;
        }

        public IContainerBuilder Register<T>(Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _container.Register(Component.For<T>().ImplementedBy<T>().LifestyleSingleton().AddNamedConstructorInjectionSupport<T, T>());
                    break;
                case Scope.Transient:
                    _container.Register(Component.For<T>().ImplementedBy<T>().LifestyleTransient().AddNamedConstructorInjectionSupport<T, T>());
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
                    _container.Register(Component.For<T>().ImplementedBy<T>().LifestyleSingleton().Named(name).AddNamedConstructorInjectionSupport<T, T>());
                    break;
                case Scope.Transient:
                    _container.Register(Component.For<T>().ImplementedBy<T>().LifestyleTransient().Named(name).AddNamedConstructorInjectionSupport<T, T>());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            _container.Register(Component.For<TInterface>().ImplementedBy<TImplementation>().LifestyleTransient().AddNamedConstructorInjectionSupport<TInterface, TImplementation>());
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(string name) where TInterface : class where TImplementation : class, TInterface
        {
            _container.Register(Component.For<TInterface>().ImplementedBy<TImplementation>().LifestyleTransient().Named(name).AddNamedConstructorInjectionSupport<TInterface, TImplementation>());
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(Scope scope) where TInterface : class where TImplementation : class, TInterface
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _container.Register(Component.For<TInterface>().ImplementedBy<TImplementation>().LifestyleSingleton().AddNamedConstructorInjectionSupport<TInterface, TImplementation>());
                    break;
                case Scope.Transient:
                    _container.Register(Component.For<TInterface>().ImplementedBy<TImplementation>().LifestyleTransient().AddNamedConstructorInjectionSupport<TInterface, TImplementation>());
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
                    _container.Register(Component.For<TInterface>().ImplementedBy<TImplementation>().LifestyleSingleton().Named(name).AddNamedConstructorInjectionSupport<TInterface, TImplementation>());
                    break;
                case Scope.Transient:
                    _container.Register(Component.For<TInterface>().ImplementedBy<TImplementation>().LifestyleTransient().Named(name).AddNamedConstructorInjectionSupport<TInterface, TImplementation>());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory) where T : class
        {
            _container.Register(Component.For<T>().UsingFactoryMethod<T>(x => factory(new WindsorReadOnlyContainer(x))).LifestyleTransient().AddNamedConstructorInjectionSupport<T, T>());
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, string name) where T : class
        {
            _container.Register(Component.For<T>().UsingFactoryMethod<T>(x => factory(new WindsorReadOnlyContainer(x))).LifestyleTransient().Named(name).AddNamedConstructorInjectionSupport<T, T>());
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _container.Register(Component.For<T>().UsingFactoryMethod<T>(x => factory(new WindsorReadOnlyContainer(x))).LifestyleSingleton().AddNamedConstructorInjectionSupport<T, T>());
                    break;
                case Scope.Transient:
                    _container.Register(Component.For<T>().UsingFactoryMethod<T>(x => factory(new WindsorReadOnlyContainer(x))).LifestyleTransient().AddNamedConstructorInjectionSupport<T, T>());
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
                    _container.Register(Component.For<T>().UsingFactoryMethod<T>(x => factory(new WindsorReadOnlyContainer(x))).LifestyleSingleton().Named(name).AddNamedConstructorInjectionSupport<T, T>());
                    break;
                case Scope.Transient:
                    _container.Register(Component.For<T>().UsingFactoryMethod<T>(x => factory(new WindsorReadOnlyContainer(x))).LifestyleTransient().Named(name).AddNamedConstructorInjectionSupport<T, T>());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }
    }
}