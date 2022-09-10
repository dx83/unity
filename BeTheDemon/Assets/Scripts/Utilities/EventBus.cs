using System;
using System.Collections.Generic;

public class EventBus
{
    static Dictionary<Type, List<Delegate>> bus = new Dictionary<Type, List<Delegate>>();

    public static void Reset()
    {
        bus = new Dictionary<Type, List<Delegate>>();
    }

    public static void Publish<T>(T e)                  // Event run
    {
        var evType = typeof(T);
        if (bus.ContainsKey(evType) == false)
            return;

        var list = bus[evType];
        foreach (var f in list)
            (f as System.Action<T>)?.Invoke(e);
    }

    public static void Subscribe<T>(Action<T> func)     // Event register
    {
        var evType = typeof(T);
        if (bus.ContainsKey(evType) == false)
            bus.Add(evType, new List<Delegate>());

        bus[evType].Add(func);
    }

    public static void Unsubscribe<T>(Action<T> func)   // Event erase
    {
        var evType = typeof(T);
        if (bus.ContainsKey(evType) == false)
            return;

        var list = bus[evType];
        list.Remove(func);
    }
}
