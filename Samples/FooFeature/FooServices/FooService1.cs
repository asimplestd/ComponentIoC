using UnityEngine;

public interface IFooService1
{
    string FooService1Api();
}

public class FooService1 : MonoBehaviour, IFooService1
{
    public string FooService1Api()
    {
        return $"This is {typeof(FooService1)}. My HashCode is {GetHashCode()}";
    }
}
