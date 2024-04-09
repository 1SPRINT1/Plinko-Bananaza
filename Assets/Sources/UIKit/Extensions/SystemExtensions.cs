using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public static class SystemExtensions {
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection, int iterations) {

        if (collection.Count() < 2) return collection;
            
        var rnd = new System.Random();

        for (int i = 0; i < iterations; i++) {
            collection = collection
                .OrderBy(s => rnd.Next())
                .ToArray();
        }

        return collection;
    }
    public static IDisposable Subscribe<TItem>(this HashSet<TItem> source, TItem item) {
        source.Add(item);
        return new SubscriptionDispose(() => {
            if (source.Contains(item))
                source.Remove(item);
        });
    }

    public static bool TryGetItem<TSource, TItem>(this IEnumerable<TSource> source, out TItem item) where TItem : TSource {
        item = source.OfType<TItem>().FirstOrDefault();

        return item != null;
    }
    
    public static Type[] GetAssembliesTypes(this AppDomain domain) {
        return domain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .ToArray();
    }
    
    public static void OnType<TType>(this IEnumerable<Type> source, Action<Type> onItem) {
        Type interfaceType = typeof(TType);
        
        source
            .Where(type => !type.IsAbstract && type.IsClass && interfaceType.IsAssignableFrom(type))
            .Each(t => onItem?.Invoke(t));
    }

    public static void BindAsSingle<TType, TService>(this DiContainer container) where TType: class where TService: class, TType {
        container
            .Bind<TType>()
            .To<TService>()
            .AsSingle();
    }
    
    public static void BindAsSingle<TType>(this DiContainer container)  {
        container
            .BindInterfacesAndSelfTo<TType>()
            .AsSingle();
    }

    public static void BindAsSingleFromInstanceMono<TType>(this DiContainer container) where TType : MonoBehaviour {
        container.BindAsSingleFromInstance(Object.FindObjectOfType<TType>());
    }
    
    public static void BindAsSingleFromInstance<TType>(this DiContainer container, TType instance) {
        container.BindAsSingleFromInstanceType(instance);
    }
    
    public static void BindAsSingleFromInstanceType(this DiContainer container, object instance) {
        container
            .BindInterfacesAndSelfTo(instance.GetType())
            .FromInstance(instance);
    }
    
}
