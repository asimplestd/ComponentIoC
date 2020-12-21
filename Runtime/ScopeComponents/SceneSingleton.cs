using UnityEngine;
using UnityEngine.Assertions;

namespace Simple.ComponentIoC.ScopeComponents
{
    public class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        var type = typeof(T);
                        _instance = GameObject.FindObjectOfType(type) as T;
                        if (_instance == null)
                        {
                            _instance = new GameObject("_" + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                        }
                    }

                    _instance.OnInitialize();
                }

                return _instance;
            }
        }

        public static bool Exists
        {
            get { return _instance != null; }
        }

        private static object _syncRoot = new Object();
        private static volatile T _instance = null;

        protected virtual void OnInitialize() { }
        protected virtual void OnDispose() { }

        private void Awake()
        {
            Assert.IsNull(_instance);
            _instance = this as T;
        }

        private void OnApplicationQuit()
        {
            _instance.OnDispose();
            _instance = null;
        }
    }
}