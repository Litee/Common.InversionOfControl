# Common.InversionOfControl
=========================

Simple generic wrapper around several IoC containers. 

Supported containers: Unity, Autofac, Ninject

Existing features:
* Instance/Implementation/Lambda registration (generics-only)
* Singleton and Transient scopes support
* Constructor injection, including named one (via *NamedDependency* attribute)

## TODO:
- Dependency injection
- Optional dependencies support
- Mark one of multiple constructors for injection
- Resolve multiple instances
- etc
