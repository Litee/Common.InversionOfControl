using System;

namespace Common.InversionOfControl
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class NamedDependencyAttribute : Attribute
    {
        public NamedDependencyAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}