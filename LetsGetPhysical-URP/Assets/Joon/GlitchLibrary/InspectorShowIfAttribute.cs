using UnityEngine;

public class InspectorShowIfAttribute : PropertyAttribute
{
    public readonly string showIf;

    public InspectorShowIfAttribute(string showIf)
    {
        this.showIf = showIf;
    }
}
