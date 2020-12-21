using System;

namespace Simple.ComponentIoC
{
    public interface IComponentContextBuilder
    {
        IContextRegistrationBuilder RegisterInstance<T>(T instance) where T : class;
        IContextRegistrationBuilder RegisterType(Type t);
        IContextRegistrationBuilder RegisterType<T>();
    }
}
