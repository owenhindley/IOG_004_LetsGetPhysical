using UnityEngine;

public static class MathExtension
{
    public static bool CompareWithTolerance(float a, float b, float tolerance)
    {
        return Mathf.Abs(a - b) < tolerance;
    } 
    
    public static bool CompareWithTolerance(Vector3 a, Vector3 b, float tolerance)
    {
        return Vector3.Distance(a,b) < tolerance;
    }
    
    public static Vector3 NearestPointOnLine(Vector3 linePnt, Vector3 lineDir, Vector3 pnt)
    {
        lineDir.Normalize();//this needs to be a unit vector
        var v = pnt - linePnt;
        var d = Vector3.Dot(v, lineDir);
        return linePnt + lineDir * d;
    }
    
    public static Vector3 NearestPointOnFiniteLine(Vector3 start, Vector3 end, Vector3 pnt)
    {
        var line = (end - start);
        var len = line.magnitude;
        line.Normalize();
   
        var v = pnt - start;
        var d = Vector3.Dot(v, line);
        d = Mathf.Clamp(d, 0f, len);
        return start + line * d;
    }

    
    public static bool IsPointOnLine(Vector3 linePnt, Vector3 lineDir, Vector3 pnt, float tolerance)
    {
       var closestPoint =  NearestPointOnLine(linePnt, lineDir, pnt);
       return Vector3.Distance(closestPoint, pnt) < tolerance;
    }
    
    public static float DistanceToRay(Ray ray, Vector3 point)
    {
        return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
    }
    
    public static float DistanceToRay(Vector3 rayDirection, Vector3 rayOrigin, Vector3 point)
    {
        return Vector3.Cross(rayDirection, point - rayOrigin).magnitude;
    }

}
