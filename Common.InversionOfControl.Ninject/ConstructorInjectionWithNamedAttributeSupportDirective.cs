using System.Linq;
using System.Reflection;
using Ninject.Injection;
using Ninject.Planning.Directives;
using Ninject.Planning.Targets;

namespace Common.InversionOfControl.Ninject
{
    internal class ConstructorInjectionWithNamedAttributeSupportDirective : ConstructorInjectionDirective
    {
        public ConstructorInjectionWithNamedAttributeSupportDirective(ConstructorInfo constructor, ConstructorInjector injector) : base(constructor, injector)
        {
        }

        protected override ITarget[] CreateTargetsFromParameters(ConstructorInfo method)
        {
            return method.GetParameters().Select(parameter => new ParameterWithNamedAttributeSupportTarget(method, parameter)).ToArray();
        }
    }
}