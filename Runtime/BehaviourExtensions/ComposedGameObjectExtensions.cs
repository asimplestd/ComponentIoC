using Asimple.ComponentIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ComposedGameObjectExtensions
{
    public static IEnumerable<Component> GetChildren(this GameObject host)
    {
        for (int i = 0; i < host.transform.childCount; i++)
        {
            var child = host.transform.GetChild(i);
            yield return child.GetComponent<MonoBehaviour>();
        }
    }

    public static bool HasChild<TChild>(this GameObject host) where TChild : Component
    {
        return GetChildren(host).Any(c => c is TChild);
    }

    public static TChild AddChild<TChild>(this GameObject host, Action<IComponentContextBuilder> contextBuilderAction = null) where TChild : Component
    {
        return (TChild)AddChild(host, typeof(TChild), contextBuilderAction);
    }

    public static Component AddChild(this GameObject host, Type childType, Action<IComponentContextBuilder> contextBuilderAction = null)
    {
        var childGo = new GameObject(childType.Name);
        childGo.transform.SetParent(host.transform);

        if (contextBuilderAction != null)
        {
            childGo.InitComponentContext(contextBuilderAction, childType);
        }

        var component = childGo.AddComponent(childType);
        return component;
    }

    public static void RemoveChild(this GameObject host, Component child)
    {
        UnityEngine.Object.Destroy(child.gameObject);
    }
}
