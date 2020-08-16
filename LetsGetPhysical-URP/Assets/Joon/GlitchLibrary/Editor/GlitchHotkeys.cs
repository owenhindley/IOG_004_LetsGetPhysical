using UnityEditor;
using UnityEngine;


public class GlitchHotkeys
{
    [MenuItem("GlitchLibrary/Toggle Selected GameObject %e")]
    static void Disable()
    {
        foreach (var item in Selection.gameObjects)
        {
            item.SetActive(!item.activeSelf);
            EditorUtility.SetDirty(item);
        }
    }

    [MenuItem("GlitchLibrary/Enable Selected GameObject #&RIGHT")]
	static void Enable()
    {
        foreach (var item in Selection.gameObjects)
        {
            item.SetActive(true);
            EditorUtility.SetDirty(item);
        }
    }

    [MenuItem("GlitchLibrary/Reset PlayerPrefs")]
	static void ResetPPRefs()
    {
        PlayerPrefs.DeleteAll();
    }

    

}