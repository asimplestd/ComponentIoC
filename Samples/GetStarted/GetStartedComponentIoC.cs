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

        private void Start()
        {
            AddClientComponent();
            AddNestedGoWithComponent();
        }

        private void AddClientComponent()
        {
            gameObject.AddComponent<GetStartedClientCompoent>();
        }

        private void AddNestedGoWithComponent()
        {
            var childGo = new GameObject("GetStartedClientNestedCompoent Go");
            childGo.transform.SetParent(transform);
            childGo.AddComponent<GetStartedClientNestedCompoent>();
        }
    }
}
