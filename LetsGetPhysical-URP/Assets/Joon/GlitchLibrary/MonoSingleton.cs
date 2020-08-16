using System;
using UnityEngine;

public class MonoSingleton : MonoBehaviour
{
    //[HideInInspector]
    //public bool InstanceLivesInScene;
}

public class MonoSingleton<T> : MonoSingleton where T : MonoSingleton
{
    //The instance
    private static T _instance;

    //NOTE: using a bool here to avoid Unity's custom == null operation, everytime you use the instance.
    private static bool instantiated;
    private static bool isAwake;
    private static bool isQuitting;
    
    //NOTE: Locking to avoid multiple instantiations.
    private static object _lock = new object();

    private static bool debug = false;
    private static bool debugRuntimeCreation = true;
    protected static bool overwrite = true;
    static bool _wasDestroyed;
    
    
    public static T i { get { return instance; } }

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        instance = null;
    }
    
    public void Awake()
    {
        instance = this as T;
        if (!_wasDestroyed)
        {
            AwakeSingleton();
            isAwake = true;
        }
    } 

    

    public static T instance
    {
        get
        {
            lock (_lock)
            {
                if (!instantiated)
                {
                    var type = typeof(T);

                    if (debug || debugRuntimeCreation) Debug.Log("[MonoSingleton] instancing " + type);
                    if (!Application.isPlaying)
                    {
                        Debug.LogError("Trying to instantiate singleton in editor?");
                        throw new Exception();
                    }
             
                    //Default instantiation, if attribute is not found.
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(singleton) " + type.ToString();


                    if (type.ToString() == "InputHelper")
                    {
                        for (int j = 0; j < 50; j++)
                        {
                            Debug.LogError("ROGUE INPUTHELPER DETECTED !!!!!!!!!!!!!!!!!!!!!!!!!!!");    
                        }
                    }

                    if (_instance.transform.parent != null)
                    {
                        _instance.transform.parent = null;
                        if(debug) Debug.Log("[MonoSingleton] Forced [" + _instance.name + "] to be a root object.");
                    }

                    //DontDestroyOnLoad(singleton);

                    instantiated = true;
                }
                    
                return _instance;
            }
        }
        set
        {
            lock (_lock)
            {
                if (instantiated)
                {
                    if (_instance != value)
                    {
                        var type = typeof(T);

                        //Remove instance if it's set to null, to reset a singleton.
                        if (value == null)
                        {
                            if(debug) Debug.LogWarning("[MonoSingleton] Removing instance of [" + type + "] " +
                                "on [" + _instance.name + "] due to the instance being set to null.");
                            
                            DestroyImmediate(_instance);
                            instantiated = false;
                        }
                        
                        // OVERWRITE
                        else if(overwrite)
                        {
                            Debug.LogWarning("[MonoSingleton] overwriting [" + type + "]");
                            _instance.name = "DEAD MONOSINGLETON! [" + type + "]";
                            //Destroy(_instance.gameObject);
                            _instance = value;
                        }
                        
                        // KEEP
                        else
                        {
                            Debug.LogWarning("[MonoSingleton] Removing duplicate instance of [" + type + "] on [" + value.name + "].");
                            Destroy(value.gameObject);
                            _wasDestroyed = true;
                        }

                        //remove quitting flag, as we're not quitting...
                    }
                }
                else
                {
                    _instance = value;

                    var type = typeof(T);

                    if (!instantiated && _instance != null)
                    {
                        if(debug) Debug.Log("[MonoSingleton] An instance of [" + type +
                            "] has been registered on [" + _instance.name + "].");

                        //DontDestroyOnLoad(_instance.gameObject);    

                        instantiated = true;
                    }
                }
            }
        }
    }


    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
            instantiated = false;
            isAwake = false;
        }
        OnDestroySingleton();
    }

    /// <summary>
    /// In ondestroy methods, to deregister events and prevent errors in the editor
    /// Check this for true before deregistering
    /// </summary>
    /// <returns></returns>
    public static bool IsInstantiated()
    {
        return instantiated;
    }

    public static bool IsInstantiatedAndAwake()
    {
        return IsInstantiated() && isAwake && !isQuitting;
    }
    
    public virtual void AwakeSingleton()
    {
    }
    public virtual void OnDestroySingleton()
    {
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
