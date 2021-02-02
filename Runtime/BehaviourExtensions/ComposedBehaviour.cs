using Asimple.ComponentIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ComposedBehaviour : ComposedBehaviour<bool>
{
    protected void Complete()
    {
        Complete(true);
    }
}

public abstract class ComposedBehaviour<TResult> : ObservableBehaviour<TResult>
{
    protected IEnumerable<Component> GetChildren()
    {
        return ComposedGameObjectExtensions.GetChildren(gameObject);
    }

    protected bool HasChild<TChild>() where TChild : Component
    {
        return GetChildren().Any(c => c is TChild);
    }

    protected TChild AddChild<TChild>(Action<IComponentContextBuilder> contextBuilderAction = null) where TChild : Component
    {
        return (TChild)AddChild(typeof(TChild), contextBuilderAction);
    }

    protected Component AddChild(Type childType, Action<IComponentContextBuilder> contextBuilderAction = null)
    {
        return ComposedGameObjectExtensions.AddChild(gameObject, childType, contextBuilderAction);
    }

    protected void RemoveChild(Component child)
    {
        Destroy(child.gameObject);
    }

    protected TChild AddChildWithParameters<TChild>(params object[] parameters) where TChild : Component
    {
        return (TChild)AddChildWithParameters(typeof(TChild), parameters);
    }

    protected Component AddChildWithParameters(Type childType, params object[] parameters)
    {
        Action<IComponentContextBuilder> contextBuilderAction = b =>
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                b.RegisterInstance(parameters[i]);
            }
        };

        return AddChild(childType, contextBuilderAction);
    }

}
