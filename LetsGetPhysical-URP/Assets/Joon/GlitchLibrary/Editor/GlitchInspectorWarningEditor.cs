using UnityEditor;

[CustomEditor(typeof(GlitchInspectorWarning))]
public class GlitchInspectorWarningEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        var instance = (target as GlitchInspectorWarning);
        EditorGUILayout.HelpBox(instance.warning, MessageType.Info);

    }

}
