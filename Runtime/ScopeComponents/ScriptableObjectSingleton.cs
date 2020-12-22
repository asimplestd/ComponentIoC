using UnityEngine;

namespace Asimple.ComponentIoC.ScopeComponents
{
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject
        where T : ScriptableObjectSingleton<T>
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (_instance == null)
                    {
                        _instance = CreateInstance<T>();
                    }
                    _instance.OnInitialize();
                }

                return _instance;
            }
        }

        private static T _instance;

        protected virtual void OnInitialize()
        { }

        protected virtual void OnDispose()
        { }

        ///
        /// Unity
        ///

        private void OnDestroy()
        {
            if (_instance)
                _instance.OnDispose();

            _instance = null;
        }
    }
}