using Asimple.ComponentIoC.ScopeComponents;
using UnityEngine;

namespace Asimple.ComponentIoC.Internal
{
    internal class ExternallyOwnedHost : UnitySingleton<ExternallyOwnedHost>
    {
        public static GameObject GameObject => Instance.gameObject;
    }
}
