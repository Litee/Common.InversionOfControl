using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;

namespace Common.InversionOfControl.CastleWindsor
{
    internal static class WindsorContainerExtensions
    {
        internal static ComponentRegistration<TInterface> AddNamedConstructorInjectionSupport<TInterface, TImplementation>(this ComponentRegistration<TInterface> registrationBuilder) where TImplementation : class where TInterface : class
        {
            var constructors = typeof(TImplementation).GetConstructors();
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters().Where(x => x.HasAttribute<NamedDependencyAttribute>());
                foreach (var parameter in parameters)
                {
                    var attribute = parameter.GetAttribute<NamedDependencyAttribute>();
                    registrationBuilder.DependsOn(ServiceOverride.ForKey(parameter.Name).Eq(attribute.Name));
                }
            }
            return registrationBuilder;
        }
    }
}