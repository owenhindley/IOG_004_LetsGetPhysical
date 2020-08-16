using UnityEngine;

public class EditorSafeUtils : MonoBehaviour
{
    public static void SetSceneDirty(GameObject gameObject)
    {
#if UNITY_EDITOR
        if( !Application.isPlaying ) UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty( gameObject.scene );
#endif
   }
    
    public static void SetDirty(GameObject gameObject)
    {
#if UNITY_EDITOR
	    if( !Application.isPlaying ) 
            UnityEditor.EditorUtility.SetDirty(gameObject);
#endif
    }
    
    public static void SetDirty(Object o)
    {
#if UNITY_EDITOR
        if( !Application.isPlaying ) 
            UnityEditor.EditorUtility.SetDirty(o);
#endif
    }
}
