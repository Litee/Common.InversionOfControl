using System;
using Autofac;

namespace Common.InversionOfControl.Autofac
{
    public class AutofacContainerBuilder : IContainerBuilder
    {
        private readonly ContainerBuilder _containerBuilder;

        public AutofacContainerBuilder()
        {
            _containerBuilder = new ContainerBuilder();
        }

        public IDisposableContainer Build()
        {
            return new AutofacReadOnlyContainer(_containerBuilder.Build());
        }

        public IContainerBuilder RegisterSingleton<T>(T instance) where T : class
        {
            _containerBuilder.RegisterInstance(instance).SingleInstance();
            return this;
        }

        public IContainerBuilder RegisterSingleton<T>(T instance, string name) where T : class
        {
            _containerBuilder.RegisterInstance(instance).SingleInstance().Named<T>(name);
            return this;
        }

        public IContainerBuilder Register<T>() where T : class
        {
            _containerBuilder.RegisterType<T>().InstancePerDependency().AddNamedConstructorInjectionSupport();
            return this;
        }

        public IContainerBuilder Register<T>(string name) where T : class
        {
            _containerBuilder.RegisterType<T>().Named<T>(name).InstancePerDependency().AddNamedConstructorInjectionSupport();
            return this;
        }

        public IContainerBuilder Register<T>(Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _containerBuilder.RegisterType<T>().SingleInstance().AddNamedConstructorInjectionSupport();
                    break;
                case Scope.Transient:
                    _containerBuilder.RegisterType<T>().InstancePerDependency().AddNamedConstructorInjectionSupport();
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
                    _containerBuilder.RegisterType<T>().Named<T>(name).SingleInstance().AddNamedConstructorInjectionSupport();
                    break;
                case Scope.Transient:
                    _containerBuilder.RegisterType<T>().Named<T>(name).InstancePerDependency().AddNamedConstructorInjectionSupport();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>().AddNamedConstructorInjectionSupport();
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(string name) where TInterface : class where TImplementation : class, TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>().Named<TInterface>(name).AddNamedConstructorInjectionSupport();
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(Scope scope) where TInterface : class where TImplementation : class, TInterface
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _containerBuilder.RegisterType<TImplementation>().As<TInterface>().SingleInstance().AddNamedConstructorInjectionSupport();
                    break;
                case Scope.Transient:
                    _containerBuilder.RegisterType<TImplementation>().As<TInterface>().InstancePerDependency().AddNamedConstructorInjectionSupport();
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
                    _containerBuilder.RegisterType<TImplementation>().As<TInterface>().SingleInstance().Named<TInterface>(name).AddNamedConstructorInjectionSupport();
                    break;
                case Scope.Transient:
                    _containerBuilder.RegisterType<TImplementation>().As<TInterface>().InstancePerDependency().Named<TInterface>(name).AddNamedConstructorInjectionSupport();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory) where T : class
        {
            _containerBuilder.Register(context => factory(new AutofacReadOnlyContainer(context))).InstancePerDependency();
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, string name) where T : class
        {
            _containerBuilder.Register(context => factory(new AutofacReadOnlyContainer(context))).InstancePerDependency().Named<T>(name);
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _containerBuilder.Register(context => factory(new AutofacReadOnlyContainer(context))).SingleInstance();
                    break;
                case Scope.Transient:
                    _containerBuilder.Register(context => factory(new AutofacReadOnlyContainer(context))).InstancePerDependency();
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
                    _containerBuilder.Register(context => factory(new AutofacReadOnlyContainer(context))).SingleInstance().Named<T>(name);
                    break;
                case Scope.Transient:
                    _containerBuilder.Register(context => factory(new AutofacReadOnlyContainer(context))).InstancePerDependency().Named<T>(name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }
    }
}