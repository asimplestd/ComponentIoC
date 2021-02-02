using UnityEngine;

namespace Asimple.ComponentIoC.GetStarted
{
    public class GetStartedClientCompoent : MonoBehaviour
    {
        private void Awake()
        {
            var context = gameObject.GetContext();

            var service1 = context.Resolve<IGetStartedService1>();
            Debug.Log($"{typeof(IGetStartedService1)} {service1} service has been resolved.", gameObject);
        }
    }
}
