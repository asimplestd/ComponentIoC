using UnityEngine;

namespace Asimple.ComponentIoC.FooFeature
{
    public class FooController : ComposedBehaviour
    {
        private IFooService1 fooService1;
        private IFooService2 fooService2;

        private void Awake()
        {
            // gameObject.GetContext() is an extension method, which ties to find the closest ComponentContext component
            var context = gameObject.GetContext();

            // Resolve services according FooConfiguration
            fooService1 = context.Resolve<IFooService1>();
            fooService2 = context.Resolve<IFooService2>();
        }

        private void Start()
        {
            Debug.Log($"FooController::Start() - {fooService1.FooService1Api()}");
            // Console output: FooController::Start() - This is FooService1. My HashCode is -2676
            Debug.Log($"FooController::Start() - {fooService2.FooService2Api()}");
            // Console output: FooController::Start() - This is FooService2.My HashCode is -3170

            // AddChild<FooChildController> method does:
            // 1. Attaches child gameobject to this.gameObject
            // 2. Adds FooChildController component to child gameobject
            // Pay attension, 'AddChild' method doesn't attach ComponentContext to the child gameobject since it makes no sense with no custom configuration.
            AddChild<FooChildController>();
        }
    }
}