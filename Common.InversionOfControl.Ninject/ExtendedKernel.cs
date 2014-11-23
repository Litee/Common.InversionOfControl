using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using Ninject.Planning.Strategies;

namespace Common.InversionOfControl.Ninject
{
    public class ExtendedKernel : StandardKernel
    {
        public ExtendedKernel(params INinjectModule[] modules) : base(modules)
        {
        }

        public ExtendedKernel(INinjectSettings settings, params INinjectModule[] modules) : base(settings, modules)
        {
        }

        protected override void AddComponents()
        {
            base.AddComponents();
            Components.Remove<IPlanningStrategy, ConstructorReflectionStrategy>();
            Components.Add<IPlanningStrategy, ConstructorReflectionWithNamedAttributeSupportStrategy>();
        }

        public override IEnumerable<IBinding> GetBindings(Type service)
        {
            List<IBinding> enumerable = base.GetBindings(service).ToList();
            return enumerable;
        }

        public override IEnumerable<object> Resolve(IRequest request)
        {
            IEnumerable<object> enumerable = base.Resolve(request);
            return enumerable;
        }
    }
}