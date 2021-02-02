using UnityEngine;

namespace Asimple.ComponentIoC.FooFeature
{
    public class FooChildController : ComposedBehaviour
    {
        private IFooService1 fooService1;
        private IFooService2 fooService2;

        private void Awake()
        {
            // gameObject.GetContext() is an extension method, which ties to find the closest ComponentContext component
            var context = gameObject.GetContext();

            // Resolve services according FooConfiguration
            // According to FooConfiguration, IFooService2 is registered as InstancePerDependency.
            // Hence, 'context.Resolve<IFooService2>()' creates new instance of FooService2, according to FooConfiguration
            fooService1 = context.Resolve<IFooService1>();
            fooService2 = context.Resolve<IFooService2>();
        }

        private void Start()
        {
            Debug.Log($"FooChildController::Start() - {fooService1.FooService1Api()}");
            // Console output: FooChildController::Start() - This is FooService1.My HashCode is -3164
            Debug.Log($"FooChildController::Start() - {fooService2.FooService2Api()}");
            // Console output: FooChildController::Start() - This is FooService2.My HashCode is -3182

            AddChild<FooChildOfChildController>(b => 
                {
                    b.RegisterInstance(fooService2).As<IFooService2>();
                });
        }
    }
}
