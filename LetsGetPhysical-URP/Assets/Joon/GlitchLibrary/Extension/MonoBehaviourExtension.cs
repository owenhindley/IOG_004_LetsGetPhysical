using UnityEngine;

public static class MonoBehaviourExtension
{
    public static T RequireComponent<T>(this MonoBehaviour monoBehaviour)
    {
        var result = monoBehaviour.GetComponent<T>();
        if(result == null) Debug.LogError("Missing component!", monoBehaviour.gameObject);
        return result;
    } 
}
