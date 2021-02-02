using UnityEngine;

public interface IFooService4
{
    string FooService4Api();
}

public class FooService4 : MonoBehaviour, IFooService4
{
    public string FooService4Api()
    {
        return $"This is {typeof(FooService4)}. My HashCode is {GetHashCode()}";
    }
}
