using System;
using System.Collections.Generic;

namespace Asimple.ComponentIoC.Internal
{
    internal class ContextRegistrationBuilder : IContextRegistration, IContextRegistrationBuilder
    {
        private List<Type> _asTypes;

        public ContextRegistrationBuilder(object instance, Type type)
        {
            ComponentType = type;

            InstantiationStrategy = InstantiationStrategy.InstancePerDependency;
            OwnershipStrategy = OwnershipStrategy.ExternallyOwned;

            Instance = instance;
        }

        public ContextRegistrationBuilder(Type type)
        {
            OwnershipStrategy = OwnershipStrategy.ContextOwned;
            InstantiationStrategy = InstantiationStrategy.InstancePerDependency;
            ComponentType = type;
        }

        public object Instance { get; private set; }
        public Type ComponentType { get; }
        public InstantiationStrategy InstantiationStrategy { get; private set; }
        public OwnershipStrategy OwnershipStrategy { get; private set; }
        public object ContextTag { get; private set; }

        public bool ContainsType(Type type)
        {
            if (_asTypes == null)
            {
                return  ComponentType == type;
            }

            return _asTypes.Contains(type);
        }

        public IContextRegistrationBuilder As(Type type)
        {
            _asTypes = _asTypes ?? new List<Type>();
            _asTypes.Add(type);
            return this;
        }

        public IContextRegistrationBuilder As<T>()
        {
            return As(typeof(T));
        }

        public IContextRegistrationBuilder AsSelf()
        {
            _asTypes.Add(ComponentType);
            return this;
        }

        public IContextRegistrationBuilder InstancePerDependency()
        {
            InstantiationStrategy = InstantiationStrategy.InstancePerDependency;
            return this;
        }

        public IContextRegistrationBuilder SingleInstance()
        {
            InstantiationStrategy = InstantiationStrategy.InstancePerContext;
            return this;
        }

        public IContextRegistrationBuilder InScopeOf(object contextTag)
        {
            ContextTag = contextTag;
            InstantiationStrategy = InstantiationStrategy.InstancePerContext;
            return this;
        }

        public IContextRegistrationBuilder ExternallyOwned()
        {
            OwnershipStrategy = OwnershipStrategy.ExternallyOwned;
            return this;
        }
    }
}
