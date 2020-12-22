using Asimple.ComponentIoC;
using Asimple.ComponentIoC.Internal;
using System;
using UnityEngine;

public static class ComponentContextExtensions
{
    public static void InitComponentContext(this GameObject gameObject, Action<IComponentContextBuilder> configureAction, object tag = null, IComponentContext baseContext = null)
    {
        var builder = new ComponentContextBuilder();

        configureAction?.Invoke(builder);
        var configuration = builder.BuildConfiguration();

        var context = gameObject.AddComponent<ComponentContext>();
        context.enabled = true;
        context.Initialize(configuration, tag, baseContext as ComponentContext);
    }

    public static IComponentContext GetContext(this GameObject gameObject)
    {
        var context = gameObject.GetComponent<ComponentContext>();
        if (context != null)
        {
            return context;
        }

        context = gameObject.GetComponentInParent<ComponentContext>();
        return context;
    }
}

