using System;
using System.Collections.Generic;
using System.Reflection;
using Ninject.Components;
using Ninject.Injection;
using Ninject.Planning;
using Ninject.Planning.Directives;
using Ninject.Planning.Strategies;
using Ninject.Selection;

namespace Common.InversionOfControl.Ninject
{
    internal class ConstructorReflectionWithNamedAttributeSupportStrategy : NinjectComponent, IPlanningStrategy
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConstructorReflectionStrategy" /> class.
        /// </summary>
        /// <param name="selector">The selector component.</param>
        /// <param name="injectorFactory">The injector factory component.</param>
        public ConstructorReflectionWithNamedAttributeSupportStrategy(ISelector selector, IInjectorFactory injectorFactory)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (injectorFactory == null) throw new ArgumentNullException("injectorFactory");

            Selector = selector;
            InjectorFactory = injectorFactory;
        }

        /// <summary>
        ///     Gets the selector component.
        /// </summary>
        public ISelector Selector { get; private set; }

        /// <summary>
        ///     Gets the injector factory component.
        /// </summary>
        public IInjectorFactory InjectorFactory { get; set; }

        /// <summary>
        ///     Adds a <see cref="ConstructorInjectionDirective" /> to the plan for the constructor
        ///     that should be injected.
        /// </summary>
        /// <param name="plan">The plan that is being generated.</param>
        public void Execute(IPlan plan)
        {
            if (plan == null) throw new ArgumentNullException("plan");

            IEnumerable<ConstructorInfo> constructors = Selector.SelectConstructorsForInjection(plan.Type);
            if (constructors == null)
                return;

            foreach (ConstructorInfo constructor in constructors)
            {
                plan.Add(new ConstructorInjectionWithNamedAttributeSupportDirective(constructor, InjectorFactory.Create(constructor)));
            }
        }
    }
}