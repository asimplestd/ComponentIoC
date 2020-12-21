using System;

namespace Simple.ComponentIoC
{
    public interface IContextRegistrationBuilder
    {
        IContextRegistrationBuilder As(Type type);
        IContextRegistrationBuilder As<T>();
        IContextRegistrationBuilder AsSelf();
        IContextRegistrationBuilder InstancePerDependency();
        IContextRegistrationBuilder SingleInstance();
        IContextRegistrationBuilder InScopeOf(object contextTag);
        IContextRegistrationBuilder ExternallyOwned();
    }
}
