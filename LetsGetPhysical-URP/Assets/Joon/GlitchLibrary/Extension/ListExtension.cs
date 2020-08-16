using UnityEngine;
using System.Collections.Generic;

public static class ListExtension {

    public static T GetRandom<T>(this List<T> list)
    {
        if(list == null || list.Count == 0)
        {
            Debug.LogWarning("Tried to get random element of an empty array");
            return default(T);
        } 
        return (T) list[UnityEngine.Random.Range(0,list.Count)];
    }

    
    public static List<T> Clone<T>(this List<T> list)
    {
        return (List<T>) list.GetRange(0, list.Count);
    }

    
    
}