#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class SaveMeshInEditor
{
    public static void SaveAsset(Mesh m, string name)
    {
        var savePath = "Assets/" + name + ".asset";
        Debug.Log("Saved Mesh to:" + savePath);
        AssetDatabase.CreateAsset(m, savePath);
    }
}
#endif