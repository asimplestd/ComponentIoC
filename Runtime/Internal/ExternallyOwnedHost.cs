using Simple.ComponentIoC.ScopeComponents;
using UnityEngine;

namespace Simple.ComponentIoC.Internal
{
    internal class ExternallyOwnedHost : UnitySingleton<ExternallyOwnedHost>
    {
        public static GameObject GameObject => Instance.gameObject;
    }
}
