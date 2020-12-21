namespace Simple.ComponentIoC.ScopeComponents
{
    public abstract class UnitySingleton<T> : SceneSingleton<T> where T : UnitySingleton<T>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            DontDestroyOnLoad(gameObject);
        }
    }
}