using UnityEngine;

public interface IFooService3
{
    string FooService3Api();
}

public class FooService3 : MonoBehaviour, IFooService3
{
    public string FooService3Api()
    {
        return $"This is {typeof(FooService3)}. My HashCode is {GetHashCode()}";
    }
}
