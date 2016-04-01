using System;

namespace PyriteClientIntefaces
{
    [System.AttributeUsage(System.AttributeTargets.Field |
                       System.AttributeTargets.Property,
                       AllowMultiple = false)]
    public class HumanFriendlyNameAttribute : Attribute
    {
        public HumanFriendlyNameAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}
