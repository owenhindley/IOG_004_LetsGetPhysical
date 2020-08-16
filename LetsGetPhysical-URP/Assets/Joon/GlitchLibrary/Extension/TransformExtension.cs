using UnityEngine;
using System.Collections.Generic;

public static class TransformExtension
{
    public static Transform FindChildDeep(this Transform parent, string childName, bool includeDisabled = true)
    {
        return FindChildRecursive(parent, childName, includeDisabled);
    }

    private static Transform FindChildRecursive(Transform parent, string childName, bool includeDisabled)
    {
        Transform result = parent.Find(childName);
        if (result == null)
        {
            foreach (Transform child in parent)
            {
                if (!includeDisabled &&!child.gameObject.activeSelf) continue;
                result = FindChildRecursive(child, childName, includeDisabled);
                if (result != null) break;
            }
        }
        return result;
    }


    public static Transform FindChildWithTagDeep(this Transform parent, string tag, bool includeDisabled = true)
    {
        return FindChildRecursive(parent, tag, includeDisabled);
    }

    private static Transform FindChildWithTagRecursive(Transform parent, string tag, bool includeDisabled)
    {
        Transform result = (parent.CompareTag(tag) ? parent : null);
        if (result == null)
        {
            foreach (Transform child in parent)
            {
                if (!includeDisabled && !child.gameObject.activeSelf) continue;
                result = FindChildWithTagRecursive(child, tag, includeDisabled);
                if (result != null) break;
            }
        }
        return result;
    }

    public static Transform[] FindChildrenWithTagDeep(this Transform parent, string tag, bool includeDisabled = true)
    {
        List<Transform> list = new List<Transform>();
        FindChildrenWithTagRecursive(parent, list, tag, includeDisabled);
        return list.ToArray();
    }

    private static void FindChildrenWithTagRecursive(Transform parent, List<Transform> list, string tag, bool includeDisabled)
    {
        if(parent.CompareTag(tag)) 
            list.Add(parent);
        foreach (Transform child in parent)
        {
            if (!includeDisabled && !child.gameObject.activeSelf) continue;
            FindChildrenWithTagRecursive(child, list, tag, includeDisabled);
        }
    }


    public static void Clear(this Transform parent)
    {
        var children = new List<Transform>();
        foreach (Transform child in parent) children.Add(child);
        children.ForEach(
            child =>
            {
                child.SetParent(null);
                if (Application.isPlaying)
                    GameObject.Destroy(child.gameObject);
                else
                    GameObject.DestroyImmediate(child.gameObject);
            });

    }
    
    public static void ClearExcept(this Transform parent, Transform toBeSaved)
    {
        var children = new List<Transform>();
        foreach (Transform child in parent) children.Add(child);
        children.ForEach(
            child =>
            {
                if (child == toBeSaved) return;
                child.SetParent(null);
                if (Application.isPlaying)
                    GameObject.Destroy(child.gameObject);
                else
                    GameObject.DestroyImmediate(child.gameObject);
            });

    }

    public static void ResetLocal(this Transform parent)
    {
        parent.localScale = Vector3.one;
        parent.localPosition = Vector3.zero;
        parent.localRotation = Quaternion.identity;
    }

    public static void ResetLocal(this Transform parent, bool localPosition, bool localRotation, bool localScale)
    {
        if(localScale) parent.localScale = Vector3.one;
        if(localPosition) parent.localPosition = Vector3.zero;
        if(localRotation) parent.localRotation = Quaternion.identity;
    }

    public static void CopyLocal(this Transform parent, Transform source)
    {
        parent.localScale = source.localScale;
        parent.localPosition = source.localPosition;
        parent.localRotation = source.localRotation;
    }

    public static void CopyWorld(this Transform parent, Transform source)
    {
        parent.position = source.position;
        parent.rotation = source.rotation;
    }


    /// <summary>
    /// Looks at the target in 2D, using the axis to determine which rotation is needed.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="target">Target.</param>
    /// <param name="axis">Axis. Only Supports Vector3.up, Vector3.forward, and Vector3.right (NOTE: Vector3.right is untested) </param>
    /// <param name="step">Step. Used for lerping towards the target</param>
    public static void LookAt2D(this Transform parent, Vector3 target, Vector3 axis, float step = 1f)
    {
        parent.rotation = LookAt2DLerp(parent, target, axis, step);
    }

    /// <summary>
    /// Lerps towards the target in 2D, using the axis to determine which rotation is needed.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="target">Target.</param>
    /// <param name="axis">Axis. Only Supports Vector3.up, Vector3.forward, and Vector3.right (NOTE: Vector3.right is untested) </param>
    public static Quaternion LookAt2DLerp(this Transform parent, Vector3 target, Vector3 axis, float step = 1f)
    {
        Vector3 dir = target - parent.position;
        if (axis == Vector3.forward)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return Quaternion.Lerp(parent.rotation, Quaternion.AngleAxis(angle, Vector3.forward), step);
        }
        else if (axis == Vector3.up)
        {
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            return Quaternion.Lerp(parent.rotation, Quaternion.AngleAxis(angle, Vector3.up), step);
        }
        else if (axis == Vector3.right)
        {
            //NOTE: untested
            float angle = Mathf.Atan2(dir.y, dir.z) * Mathf.Rad2Deg;
            return Quaternion.Lerp(parent.rotation, Quaternion.AngleAxis(angle, Vector3.right), step);
        }
        else
        {
            Debug.LogError("Unsupported axis provided: " + axis);
        }

        return Quaternion.identity;
    }

    public static void LookAt2D(this Transform parent, Vector3 target)
    {
        Vector3 dir = target - parent.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    //NOTE: doesn't work, but it's a good idea...
    //    public static void LookAt(this Transform parent, Vector3 target, [DefaultValue("Vector3.up")] Vector3 worldUp, float step = 1f)
    //    {
    //        var axis = Vector3.Cross(parent.forward.normalized, (target - parent.position).normalized);
    //        var angle = Vector3.Angle(parent.forward, target - parent.position);
    //        parent.rotation = Quaternion.Lerp(parent.rotation, Quaternion.AngleAxis(angle, axis), step);
    //    }

    /*public static RectTransform RectTransform(this Transform parent)
    {
        return (parent as RectTransform);
    }*/

    public static FakeTransform ToFakeTransform(this Transform parent, bool local=false)
    {
        return new FakeTransform(parent, local);
    }

    public static FakeTransform FromFakeTransform(this Transform parent, FakeTransform fakeTransform, bool local = false)
    {
        if(local)
        {
            parent.localPosition = fakeTransform.position;
            parent.localRotation = fakeTransform.rotation;
            parent.localScale = fakeTransform.scale;
        }
        else
        {

            parent.position = fakeTransform.position;
            parent.rotation = fakeTransform.rotation;
            parent.localScale = fakeTransform.scale;

        }
        return new FakeTransform(parent, local);
    }

    public static Transform[] GetChildrenAsTransforms(this Transform parent)
    {
        var result = new Transform[parent.childCount];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = parent.GetChild(i);
        }
        return result;
    }

    public static Transform AddChild(this Transform parent, string name = "New GameObject")
    {
        var newGO = new GameObject().transform;
        newGO.SetParent(parent);
        newGO.ResetLocal();
        return newGO;
    }

    public static void SetLossyScaleWorld(this Transform parent, float lossyScale)
    {
        parent.localScale = Vector3.one;
        parent.localScale = (lossyScale / parent.lossyScale.x) * Vector3.one;

    }
    
    public static void SetLossyScaleWorldUniform(this Transform parent, float lossyScale)
    {
        parent.localScale = Vector3.one;
        parent.localScale = new Vector3(1f / parent.lossyScale.x, 1f / parent.lossyScale.y, 1f / parent.lossyScale.z);
        parent.localScale *= lossyScale;

    }
}
