using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public static class CommonExtensions
{
    /// <summary>
    ///     Cached collection [through for..each] iterations
    /// </summary>
    /// <param name="source">IEnumerable source</param>
    /// <param name="onSelect">Each an element of the source collection</param>
    /// <typeparam name="T">the collection type</typeparam>
    public static void Each<T>(this IEnumerable<T> source, Action<T> onSelect)
    {
        foreach (T item in source.ToArray())
            onSelect?.Invoke(item);
    }

    /// <summary>
    ///     Cached collection [through for] iterations
    /// </summary>
    /// <param name="source">IEnumerable source</param>
    /// <param name="onSelect">Each an element of the source collection</param>
    /// <typeparam name="T">the collection type</typeparam>
    public static void For<T>(this IEnumerable<T> source, Action<int, T> onSelect)
    {
        var array = source.ToArray();

        for (int index = 0; index < array.Length; index++)
        {
            onSelect?.Invoke(index, array[index]);
        }
    }

    public static bool TryGetAttribute<TAttribute>(this Type type, out TAttribute attribute)
        where TAttribute : Attribute
    {
        attribute = type.GetCustomAttributes(false).OfType<TAttribute>().FirstOrDefault();

        return attribute != null;
    }

    public static T[] JoinWith<T>(this T source, params T[] args)
    {
        List<T> list = new List<T> { source };
        list.AddRange(args);
        return list.ToArray();
    }
    
    public static string ToCamelCase(this string source) => source.Substring(0, 1).ToLower() + source.Substring(1);
    public static string GetTypeToCall(this object source) => source.GetType().ToString().ToCamelCase();
    
    public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);
    
    public static string Serialize(this object source) => source != null? JsonConvert.SerializeObject(source) : "{}";
    public static T Deserialize<T>(this string source) => JsonConvert.DeserializeObject<T>(source);
    public static T Deserialize<T>(this object source) => source.ToString().Deserialize<T>();
}