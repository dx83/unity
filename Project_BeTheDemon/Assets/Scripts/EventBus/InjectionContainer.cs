using System;
using System.Collections.Generic;
using UnityEngine;


public class Inject : Attribute { }

public class InjectionObj
{
    bool isInjected = false;
    public void Inject<T>(T o)
    {
        if (isInjected)
            return;
        
        InjectionContainer.Inject(o);
        isInjected = true;
    }
}

public class InjectionContainer : MonoBehaviour
{
    private Dictionary<Type, object> container = new Dictionary<Type, object>();
    static InjectionContainer _instance;

    // Edit -> Projct Settings -> Script Execution Order ->
    // + -> InjectionContainer -> -50
    private void Awake() { SetInstance(this); }
    public static void SetInstance(InjectionContainer ic) { _instance = ic; }


    public static void Regist<T>(T o)
    {
        _instance.container.Add(typeof(T), o);
    }

    public static object Get(Type t)
    {
        return _instance.container[t];
    }

    public static void Inject<T>(T o)
    {
        foreach (var f in typeof(T).GetFields(System.Reflection.BindingFlags.Instance |
                                              System.Reflection.BindingFlags.Public |
                                              System.Reflection.BindingFlags.NonPublic))
        {
            bool hasInjectAttr = false;
            foreach (var c in f.GetCustomAttributes(true))
            {
                if (c.GetType() == typeof(Inject))
                {
                    hasInjectAttr = true;
                    break;
                }
            }

            if (hasInjectAttr == false)
                continue;

            f.SetValue(o, Get(f.FieldType));
        }
    }
}
