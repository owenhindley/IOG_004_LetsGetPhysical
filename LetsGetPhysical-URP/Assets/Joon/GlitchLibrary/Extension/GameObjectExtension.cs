using UnityEngine;

public static class GameObjectExtension {

    public static void SetLayer(this GameObject parent, int layer, bool includeChildren = true)
    {
        parent.layer = layer;
        if (includeChildren)
        {
            foreach (Transform trans in parent.transform.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = layer;
            }
        }
    }

    public static void ToggleActive(this GameObject parent)
    {
        parent.SetActive(!parent.activeSelf);
    }
}
