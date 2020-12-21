using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simple.ComponentIoC.Internal
{
    internal class ComponentContextBuilder : IComponentContextBuilder
    {
        private readonly List<ContextRegistrationBuilder> _registrations;

        public ComponentContextBuilder()
        {
            _registrations = new List<ContextRegistrationBuilder>();
        }

        public ContextRegistrationBuilder[] BuildConfiguration()
        {
            _registrations.Reverse();
            return _registrations.ToArray();
        }

        public IContextRegistrationBuilder RegisterInstance<T>(T instance) where T : class
        {
            if (instance == null)
            {
                Debug.Log("Trying to register NULL object");
            }
            var registration = new ContextRegistrationBuilder(instance, typeof(T));
            _registrations.Add(registration);

            return registration;
        }

        public IContextRegistrationBuilder RegisterType(Type t)
        {
            var registration = new ContextRegistrationBuilder(t);
            _registrations.Add(registration);

            return registration;
        }

        public IContextRegistrationBuilder RegisterType<T>()
        {
            return RegisterType(typeof(T));
        }
    }
}
