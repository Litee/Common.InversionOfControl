using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.Unity.Utility;

namespace Common.InversionOfControl.Unity3
{
    internal class MyDefaultConstructorSelectorPolicy : ConstructorSelectorPolicyBase<InjectionConstructorAttribute>
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