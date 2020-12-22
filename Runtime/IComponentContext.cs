using System;

namespace Asimple.ComponentIoC
{
    public interface IComponentContext
    {
        object Resolve(Type serviceType);
        T Resolve<T>() where T : class;
    }
}
