using System;
using Ninject;

namespace Common.InversionOfControl.Ninject
{
    public class NinjectContainerBuilder : IContainerBuilder
    {
        private readonly KernelBase _kernel;

        public NinjectContainerBuilder()
        {
            _kernel = new ExtendedKernel();
        }

        public IDisposableContainer Build()
        {
            return new NinjectReadOnlyContainer(_kernel);
        }

        public IContainerBuilder RegisterSingleton<T>(T instance) where T : class
        {
            _kernel.Bind<T>().ToConstant(instance);
            return this;
        }

        public IContainerBuilder RegisterSingleton<T>(T instance, string name) where T : class
        {
            _kernel.Bind<T>().ToConstant(instance).Named(name);
            return this;
        }

        public IContainerBuilder Register<T>() where T : class
        {
            _kernel.Bind<T>().To<T>().InTransientScope();
            return this;
        }

        public IContainerBuilder Register<T>(string name) where T : class
        {
            _kernel.Bind<T>().To<T>().InTransientScope().Named(name);
            return this;
        }

        public IContainerBuilder Register<T>(Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _kernel.Bind<T>().To<T>().InSingletonScope();
                    break;
                case Scope.Transient:
                    _kernel.Bind<T>().To<T>().InTransientScope();
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
                    _kernel.Bind<T>().To<T>().InSingletonScope().Named(name);
                    break;
                case Scope.Transient:
                    _kernel.Bind<T>().To<T>().InTransientScope().Named(name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            _kernel.Bind<TInterface>().To<TImplementation>().InTransientScope();
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(string name) where TInterface : class where TImplementation : class, TInterface
        {
            _kernel.Bind<TInterface>().To<TImplementation>().InTransientScope().Named(name);
            return this;
        }

        public IContainerBuilder Register<TInterface, TImplementation>(Scope scope) where TInterface : class where TImplementation : class, TInterface
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _kernel.Bind<TInterface>().To<TImplementation>().InSingletonScope();
                    break;
                case Scope.Transient:
                    _kernel.Bind<TInterface>().To<TImplementation>().InTransientScope();
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
                    _kernel.Bind<TInterface>().To<TImplementation>().InSingletonScope().Named(name);
                    break;
                case Scope.Transient:
                    _kernel.Bind<TInterface>().To<TImplementation>().InTransientScope().Named(name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory) where T : class
        {
            _kernel.Bind<T>().ToMethod<T>(context => factory(new NinjectReadOnlyContainer(context.Kernel))).InTransientScope();
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, string name) where T : class
        {
            _kernel.Bind<T>().ToMethod<T>(context => factory(new NinjectReadOnlyContainer(context.Kernel))).InTransientScope().Named(name);
            return this;
        }

        public IContainerBuilder Register<T>(Func<IContainer, T> factory, Scope scope) where T : class
        {
            switch (scope)
            {
                case Scope.Singleton:
                    _kernel.Bind<T>().ToMethod<T>(context => factory(new NinjectReadOnlyContainer(context.Kernel))).InSingletonScope();
                    break;
                case Scope.Transient:
                    _kernel.Bind<T>().ToMethod<T>(context => factory(new NinjectReadOnlyContainer(context.Kernel))).InTransientScope();
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
                    _kernel.Bind<T>().ToMethod<T>(context => factory(new NinjectReadOnlyContainer(context.Kernel))).InSingletonScope().Named(name);
                    break;
                case Scope.Transient:
                    _kernel.Bind<T>().ToMethod<T>(context => factory(new NinjectReadOnlyContainer(context.Kernel))).InTransientScope().Named(name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scope");
            }
            return this;
        }
    }
}