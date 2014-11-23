using System.Linq;
using Autofac;
using Autofac.Builder;

namespace Common.InversionOfControl.Autofac
{
    internal static class ContainerExtensions
    {
        internal static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddNamedConstructorInjectionSupport<TImplementation>(this IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder) where TImplementation : class
        {
            return registrationBuilder.WithParameter((p, c) => p.GetCustomAttributes(true).OfType<NamedDependencyAttribute>().Any(), (p, c) =>
            {
                NamedDependencyAttribute namedDependency = p.GetCustomAttributes(true).OfType<NamedDependencyAttribute>().First();
                object value;
                c.TryResolveKeyed(namedDependency.Name, p.ParameterType, out value);
                return value;
            });
        }
    }
}