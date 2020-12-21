using System;

namespace Simple.ComponentIoC
{
    public enum InstantiationStrategy
    {
        InstancePerDependency,
        InstancePerContext,
    }

    public enum OwnershipStrategy
    {
        ExternallyOwned,
        ContextOwned,
    }

    public interface IContextRegistration
    {
        object Instance { get; }
        Type ComponentType { get; }
        InstantiationStrategy InstantiationStrategy { get; }
        OwnershipStrategy OwnershipStrategy { get; }
        object ContextTag { get; }

        bool ContainsType(Type type);
    }
}
