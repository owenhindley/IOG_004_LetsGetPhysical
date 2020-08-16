using UnityEngine;

public class GlitchPrefabDynamic : MonoBehaviour
{
    public GameObject prefab;
    public bool breakPrefabInstance = false;   // setting to false will lock, true will unlock
    public bool inheritLayer = true;

    public GlitchPrefabDynamicField dynamicField;
    //public GlitchPrefabDynamicField[] dynamicFields;

    [System.Serializable]
    public struct GlitchPrefabDynamicField
    {
        // should live in class
        public GameObject obj;
        public Component comp;
        public string compType;
        public string property;
        public System.Type propertyType;
        public string subproperty;
        public System.Type subpropertyType;

        public object fieldValue;
    }
}
