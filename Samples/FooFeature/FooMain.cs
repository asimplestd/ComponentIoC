using UnityEngine;

namespace Asimple.ComponentIoC.FooFeature
{
    public class FooMain : ComposedBehaviour
    {
        [SerializeField] FooService3 fooService3;
        [SerializeField] FooService4 fooService4;

        private void Awake()
        {
            // gameObject.GetContext() finds the closest 'ComponentContext' component
            var context = gameObject.GetContext();

            var contextDescription = context == null ? "NULL" : "NOT NULL";
            Debug.Log($"Component context is {contextDescription}");
            // Console output: Component context is NULL

        }

        private void Start()
        {
            // AddChild<FooController> method does next:
            // 1. Attaches child gameobject to this.gameObject
            // 2. Adds FooChildController component to child gameobject
            // 3. Adds ComponentContext with 'FooConfiguration.ConfigureBuilder(b, fooService3, fooService4)' configuration
            AddChild<FooController>(b => FooConfiguration.ConfigureBuilder(b, fooService3, fooService4));
        }
    }
}