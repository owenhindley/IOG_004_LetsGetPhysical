using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatRange))]
public class FloatRangeDrawer : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) 
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        //var value = Vector2(property.floatValue)
        var values = new float[]{
            property.FindPropertyRelative("min").floatValue,
            property.FindPropertyRelative("max").floatValue
        };
        var names = new GUIContent[]{new GUIContent("-"),new GUIContent("+")};
        EditorGUI.BeginChangeCheck();
        EditorGUI.MultiFloatField(position, names,values);
        if(EditorGUI.EndChangeCheck())
        {
            property.FindPropertyRelative("min").floatValue = values[0];
            property.FindPropertyRelative("max").floatValue = values[1];
        }
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
   
}