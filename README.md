## ComponentIoC
Simple IoC container based on Unity components

## Get Started

Simple quick start:

[Register components with a `InitComponentContext`] and add client components.

```C#
using UnityEngine;

namespace Asimple.ComponentIoC.GetStarted
{
    public class GetStartedComponentIoC : MonoBehaviour
    {
        [SerializeField] GetStartedService6 service67;

        private void Awake()
        {
            gameObject.InitComponentContext(builder =>
            {
                builder.RegisterType<GetStartedService1>().As<IGetStartedService1>().AsSelf();
                builder.RegisterType<GetStartedService2>().As<IGetStartedService2>().AsSelf().InstancePerDependency();
                builder.RegisterType<GetStartedService3>().As<IGetStartedService3>().AsSelf().SingleInstance();
                builder.RegisterType<GetStartedService4>().As<IGetStartedService4>().InScopeOf(typeof(GetStartedComponentIoC));
                builder.RegisterType<GetStartedService5>().As<IGetStartedService5>().InScopeOf(typeof(GetStartedComponentIoC)).ExternallyOwned();
                builder.RegisterInstance(service67).As<IGetStartedService6>().As<IGetStartedService7>();
            });
        }
    }
}
```

[Resolve services from a component context (lifetime scope)]:

```C#
using UnityEngine;

namespace Asimple.ComponentIoC.GetStarted
{
    public class GetStartedClientCompoent : MonoBehaviour
    {
        private void Awake()
        {
            var context = gameObject.GetContext();
            var service1 = context.Resolve<IGetStartedService1>();
        }
    }
}
```

## Supported API

[IContextRegistrationBuilder].

```C#
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
```

[IComponentContext].

```C#
    public interface IComponentContext
    {
        object Resolve(Type serviceType);
        T Resolve<T>() where T : class;
    }
```

[IComponentContext].

```C#
    public interface IComponentContext
    {
        object Resolve(Type serviceType);
        T Resolve<T>() where T : class;
    }
```

## Behaviour Extensions

ComposedBehaviour, ObservableBehaviour provide friendly API for building hierarchical flow

[Use AddChild to add child gameObject with requiered component]. You can add custom configuration as a parameter:

```C#
using UnityEngine;

namespace Asimple.ComponentIoC.FooFeature
{
    public class FooComponent : ComposedBehaviour
    {
        [SerializeField] FooService3 fooService3;
        [SerializeField] FooService4 fooService4;

        private void Start()
        {
            AddChild<FooChildComponent>(b =>
                {
                builder.RegisterType<GetStartedService1>().As<IGetStartedService1>().AsSelf();
                builder.RegisterType<GetStartedService2>().As<IGetStartedService2>().AsSelf().InstancePerDependency();
                builder.RegisterType<GetStartedService3>().As<IGetStartedService3>().AsSelf().SingleInstance();
                builder.RegisterType<GetStartedService4>().As<IGetStartedService4>().InScopeOf(typeof(GetStartedComponentIoC));
                builder.RegisterType<GetStartedService5>().As<IGetStartedService5>().InScopeOf(typeof(GetStartedComponentIoC)).ExternallyOwned();
                builder.RegisterInstance(service67).As<IGetStartedService6>().As<IGetStartedService7>();
                });
        }
    }
}
```

[Use Observe to wait for component async flow].

ObservableBehaviour provide friendly async API:

```C#
using UnityEngine;

namespace Asimple.ComponentIoC.FooFeature
{
    public class FooComponent : ComposedBehaviour
    {
        private async void Start()
        {
            var result = await AddChild<FooObservableComponent>().Observe();
        }
    }
	
	public class FooObservableComponent : ObservableBehaviour
    {
        private void Start()
        {
            Complete();
        }
    }
}
```