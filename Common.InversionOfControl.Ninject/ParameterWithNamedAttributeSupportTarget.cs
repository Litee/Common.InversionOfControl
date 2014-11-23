using System;
using System.Linq;
using System.Reflection;
using Ninject.Planning.Bindings;
using Ninject.Planning.Targets;

namespace Common.InversionOfControl.Ninject
{
    internal class ParameterWithNamedAttributeSupportTarget : ParameterTarget
    {
        public ParameterWithNamedAttributeSupportTarget(MethodBase method, ParameterInfo site) : base(method, site)
        {
        }

        protected override Func<IBindingMetadata, bool> ReadConstraintFromTarget()
        {
            var attributes = GetCustomAttributes(typeof (NamedDependencyAttribute), true) as NamedDependencyAttribute[];

            if (attributes == null || attributes.Length == 0)
                return null;

            return metadata => attributes.All(attribute => attribute.Name == metadata.Name);
        }
    }
}