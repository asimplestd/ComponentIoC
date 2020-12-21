using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simple.ComponentIoC.Internal
{
    internal class ComponentContext : MonoBehaviour, IComponentContext
    {
        private bool _initialized;

        public IEnumerable<IContextRegistration> Config { get; private set; }
        public object Tag { get; private set; }

        [SerializeField] ComponentContext parentContext;
        private GameObject _inScopeComponentsGo;

        public void Initialize(IEnumerable<ContextRegistrationBuilder> config, object tag = null, ComponentContext baseContext = null)
        {
            if (_initialized)
            {
                throw new InvalidOperationException("ComponentContext has been initialized already.");
            }

            Config = config.ToArray();
            Tag = tag;
            parentContext = baseContext;

            _initialized = true;
        }

        public T Resolve<T>() where T : class
        {
            return Resolve(typeof(T)) as T;
        }

        public object Resolve(Type type)
        {
            var registration = FindRegistration(type, out ComponentContext registrationScope);
            if (registration == null)
            {
                Debug.LogError($"Can't find registration of {type.Name} component", gameObject);
                return null;
            }

            if (registration.Instance != null) // Instance registration
            {
                if (registration.OwnershipStrategy == OwnershipStrategy.ContextOwned)
                {
                    throw new NotImplementedException("Will be implemented later");
                }

                return registration.Instance;
            }

            GameObject host;
            if (registration.OwnershipStrategy == OwnershipStrategy.ContextOwned)
            {
                if (registration.ContextTag != null)
                {
                    var context = FindContextWithTag(registration.ContextTag);
                    if (context == null)
                    {
                        Debug.LogError($"Can't find context with tag={registration.ContextTag}");
                        return null;
                    }
                    host = context.gameObject;
                }
                else
                {
                    host = registrationScope.gameObject;
                }
            }
            else
            {
                host = ExternallyOwnedHost.GameObject;
            }


            var componentType = registration.ComponentType;
            if (registration.InstantiationStrategy == InstantiationStrategy.InstancePerDependency && registration.ContextTag == null)
            {
                var go = new GameObject(componentType.Name);
                go.transform.parent = host.transform;
                var component = go.AddComponent(componentType);
                return component;
            }
            else
            {
                var hostContext = host.GetComponent<ComponentContext>();
                if (hostContext._inScopeComponentsGo == null)
                {
                    hostContext._inScopeComponentsGo = new GameObject("_inScopeComponents_");
                    hostContext._inScopeComponentsGo.transform.parent = host.transform;
                }

                var compoent = hostContext._inScopeComponentsGo.GetComponent(componentType);
                if (compoent == null)
                {
                    compoent = hostContext._inScopeComponentsGo.AddComponent(componentType);
                }

                return compoent;
            }
        }

        private ComponentContext FindContextWithTag(object contextTag)
        {
            var context = this;
            while (context != null)
            {
                if (context.Tag == contextTag)
                {
                    return context;
                }

                context = GetParentContext(context);
            }

            return null;
        }

        private IContextRegistration FindRegistration(Type type, out ComponentContext context)
        {
            context = this;
            var registration = default(IContextRegistration);
            while (context != null)
            {
                registration = context.Config.FirstOrDefault(c => c.ContainsType(type));
                if (registration != null)
                {
                    return registration;
                }

                context = GetParentContext(context);
            }

            return null;
        }

        private static ComponentContext GetParentContext(ComponentContext context)
        {
            if (context.parentContext)
            {
                return context.parentContext;
            }

            var contextTransform = context.transform.parent;
            while (contextTransform != null)
            {
                context = contextTransform.GetComponent<ComponentContext>();
                if (context != null)
                {
                    return context;
                }

                contextTransform = contextTransform.parent;
            }

            return null;
        }
    }
}