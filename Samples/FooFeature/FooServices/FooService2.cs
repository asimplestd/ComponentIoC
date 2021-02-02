using UnityEngine;

public interface IFooService2
{
    string FooService2Api();
}

public class FooService2 : MonoBehaviour, IFooService2
{
    public string FooService2Api()
    {
        return $"This is {typeof(FooService2)}. My HashCode is {GetHashCode()}";
    }
}
