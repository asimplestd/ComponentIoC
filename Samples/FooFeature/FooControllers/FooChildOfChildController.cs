using UnityEngine;

namespace Asimple.ComponentIoC.FooFeature
{
    public class FooChildOfChildController : MonoBehaviour
    {
        private IFooService1 fooService1;
        private IFooService2 fooService2;

        private void Awake()
        {
            var context = gameObject.GetContext();

            // Resolve services according FooConfiguration
            fooService1 = context.Resolve<IFooService1>();
            // According FooConfiguration, IFooService2 is registered as InstancePerDependency, but
            // AddChild<FooChildOfChildController>(b => 
            //    {
            //        b.RegisterInstance(fooService2).As<IFooService2>();
            //    });
            // overrides registration of IFooService2
            // Hence, fooService2 will be the same as fooService2 of FooChildController
            fooService2 = context.Resolve<IFooService2>();
        }

        private void Start()
        {
            Debug.Log($"FooChildOfChildController::Start() - {fooService1.FooService1Api()}");
            // Console output: FooChildOfChildController::Start() - This is FooService1.My HashCode is -3164
            Debug.Log($"FooChildOfChildController::Start() - {fooService2.FooService2Api()}");
            // Console output: FooChildOfChildController::Start() - This is FooService2.My HashCode is -3182
        }
    }
}
