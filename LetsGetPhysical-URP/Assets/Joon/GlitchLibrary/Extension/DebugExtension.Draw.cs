using UnityEngine;


public partial class DebugExtension
{
    public static void DrawPoint(Vector3 point, float size = 1, Color? color = null, float duration =1, Transform relativeTo=null)
    {
#if UNITY_EDITOR
        Color safeColor = (color == null ? Color.red : (Color)color);

        if(relativeTo != null)
        {
            point = relativeTo.TransformPoint(point);
        }

        Debug.DrawLine(point - Vector3.left * size, point + Vector3.left * size, safeColor, duration);
        Debug.DrawLine(point - Vector3.up * size, point + Vector3.up * size, safeColor, duration);
        Debug.DrawLine(point - Vector3.forward * size, point + Vector3.forward * size, safeColor, duration);
#endif
    } 
 
    public static void DrawRay(Ray ray, float originSize = 1, float length = 100,Color? color = null, float duration =1, Transform relativeTo=null)
    {
#if UNITY_EDITOR
        Color safeColor = (color == null ? Color.red : (Color)color);
        DrawPoint(ray.origin,originSize, safeColor, duration, relativeTo);


        var p1 = ray.origin;
        var p2 = ray.origin + ray.direction * length;

        if(relativeTo != null)
        {
            p1 = relativeTo.TransformPoint(p1);
            p2 = relativeTo.TransformPoint(p2);
        }

        Debug.DrawLine(p1, p2, safeColor, duration, false);
#endif
    } 

    public static void DrawRect(Rect rect, Color? color=null, float duration =1, Transform relativeTo=null)
    {
#if UNITY_EDITOR
        Color safeColor = (color == null ? Color.red : (Color)color);

        var p1 = new Vector3(rect.x, rect.y, 0);
        var p2 = new Vector3(rect.x + rect.width, rect.y, 0);
        var p3 = new Vector3(rect.x, rect.y + rect.height, 0);
        var p4 = new Vector3(rect.x + rect.width, rect.y + rect.height, 0);

        if(relativeTo != null)
        {
            p1 = relativeTo.TransformPoint(p1);
            p2 = relativeTo.TransformPoint(p2);
            p3 = relativeTo.TransformPoint(p3);
            p4 = relativeTo.TransformPoint(p4);
        }

        Debug.DrawLine(p1, p2, safeColor, duration);
        Debug.DrawLine(p2, p4, safeColor, duration);
        Debug.DrawLine(p3, p4, safeColor, duration);
        Debug.DrawLine(p3, p1, safeColor, duration);
#endif
    }

    public static void DrawBounds(Bounds bounds, Color? color=null, float duration =1, Transform relativeTo=null)
    {
#if UNITY_EDITOR
        Color safeColor = (color == null ? Color.red : (Color)color);

        var p1 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(-1,-1,-1));
        var p2 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(1,-1,-1));
        var p3 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(-1,1,-1));
        var p4 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(1,1,-1));
        var p5 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(-1,-1,1));
        var p6 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(1,-1,1));
        var p7 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(-1,1,1));
        var p8 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(1,1,1));

        if(relativeTo != null)
        {
            p1 = relativeTo.TransformPoint(p1);
            p2 = relativeTo.TransformPoint(p2);
            p3 = relativeTo.TransformPoint(p3);
            p4 = relativeTo.TransformPoint(p4);
            p5 = relativeTo.TransformPoint(p5);
            p6 = relativeTo.TransformPoint(p6);
            p7 = relativeTo.TransformPoint(p7);
            p8 = relativeTo.TransformPoint(p8);
        }

        Debug.DrawLine(p1, p2, safeColor, duration);
        Debug.DrawLine(p2, p4, safeColor, duration);
        Debug.DrawLine(p3, p4, safeColor, duration);
        Debug.DrawLine(p3, p1, safeColor, duration);

        Debug.DrawLine(p5, p6, safeColor, duration);
        Debug.DrawLine(p6, p8, safeColor, duration);
        Debug.DrawLine(p7, p8, safeColor, duration);
        Debug.DrawLine(p7, p5, safeColor, duration);

        Debug.DrawLine(p1, p5, safeColor, duration);
        Debug.DrawLine(p2, p6, safeColor, duration);
        Debug.DrawLine(p3, p7, safeColor, duration);
        Debug.DrawLine(p4, p8, safeColor, duration);
#endif
    }
}
