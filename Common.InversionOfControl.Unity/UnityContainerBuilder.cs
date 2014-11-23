using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.Unity.Utility;

namespace Common.InversionOfControl.Unity
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
            Context.Strategies.Add(new MyStrategy(), UnityBuildStage.TypeMapping);
        }
    }

    public class MyStrategy : IBuilderStrategy
    {
        public void PreBuildUp(IBuilderContext context)
        {
            context.Policies.ClearDefault(typeof (IConstructorSelectorPolicy));
            context.Policies.SetDefault<IConstructorSelectorPolicy>(new MyDefaultUnityConstructorSelectorPolicy());
        }

        public void PostBuildUp(IBuilderContext context)
        {
            //throw new NotImplementedException();
        }

        public void PreTearDown(IBuilderContext context)
        {
            //throw new NotImplementedException();
        }

        public void PostTearDown(IBuilderContext context)
        {
            //throw new NotImplementedException();
        }
    }

    public class MyDefaultUnityConstructorSelectorPolicy : ConstructorSelectorPolicyBase<InjectionConstructorAttribute>
    {
        protected override IDependencyResolverPolicy CreateResolver(ParameterInfo parameter)
        {
            Guard.ArgumentNotNull(parameter, "parameter");

            // Resolve all DependencyAttributes on this parameter, if any
            List<NamedDependencyAttribute> attributes = parameter.GetCustomAttributes(false).OfType<NamedDependencyAttribute>().ToList();

            if (attributes.Count > 0)
            {
                // Since this attribute is defined with MultipleUse = false, the compiler will
                // enforce at most one. So we don't need to check for more.
                return new NamedTypeDependencyResolverPolicy(parameter.ParameterType, attributes[0].Name);
            }

            // No attribute, just go back to the container for the default for that type.
            return new NamedTypeDependencyResolverPolicy(parameter.ParameterType, null);
        }
    }
}