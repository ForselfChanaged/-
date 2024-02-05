
using System;
using System.Collections.Generic;
using UnityEngine;
public static class CustomExtend
{
    public static Transform FindInChild(this Transform currentTF, string componentName)
    {
        Transform t = currentTF.Find(componentName);
        if (t != null)
            return t;
        for (int i = 0; i < currentTF.childCount; i++)
        {
            Transform tr = FindInChild(currentTF.GetChild(i), componentName);
            if (tr != null)
                return tr;
        }
        return null;
    }
    public static T GetMin<T>(this IEnumerable<T> list, Func<T, float> handler) where T : Component
    {
        T result = null;
        float temp = Mathf.Infinity;
        foreach (var item in list)
        {
            if (handler(item) < temp)
            {
                temp = handler(item);
                result = item;
            }
        }
        return result;
    }
}

