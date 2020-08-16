using UnityEngine;

public static class Vector3Extension
{
    public static Vector3 withX(this Vector3 parent, float x)
    {
        return new Vector3(x, parent.y, parent.z);
    }

    public static Vector3 withXY(this Vector3 parent, float x, float y)
    {
        return new Vector3(x, y, parent.z);
    }
    
    public static Vector3 withXZ(this Vector3 parent, float x, float z)
    {
        return new Vector3(x, parent.y, z);
    }

    public static Vector3 withY(this Vector3 parent, float y)
    {
        return new Vector3(parent.x, y, parent.z);
    }

    public static Vector3 withZ(this Vector3 parent, float z)
    {
        return new Vector3(parent.x, parent.y, z);
    }

    public static Vector3 addX(this Vector3 parent, float x)
    {
        return new Vector3(parent.x + x, parent.y, parent.z);
    }

    public static Vector3 addY(this Vector3 parent, float y)
    {
        return new Vector3(parent.x, parent.y + y, parent.z);
    }

    public static Vector3 addZ(this Vector3 parent, float z)
    {
        return new Vector3(parent.x, parent.y, parent.z + z);
    }

    public static Vector3 flipX(this Vector3 parent)
    {
        return new Vector3(-parent.x, parent.y, parent.z);
    }
    
    public static Vector3 flipY(this Vector3 parent)
    {
        return new Vector3(parent.x, -parent.y, parent.z);
    }

    public static Vector3 flipZ(this Vector3 parent)
    {
        return new Vector3(parent.x, parent.y, -parent.z);
    }

    public static Vector3 addVector2(this Vector3 parent, Vector2 v2)
    {
        return new Vector3(parent.x + v2.x, parent.y + v2.y, parent.z);
    }

    public static float Distance2D(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt(Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.y - v2.y, 2));

    }

    public static float DistanceXY(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt(Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.y - v2.y, 2));

    }

    public static float DistanceXZ(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt(Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.z - v2.z, 2));

    }

    public static float Angle2D(Vector3 v1, Vector3 v2)
    {
        float result = (Mathf.Atan2(v2.y - v1.y, v2.x - v1.x) * Mathf.Rad2Deg);
        if (result < 0) result = result + 360;
        if (result > 360) result = result - 360;
        return result;
    }

    public static float Angle2DXZ(Vector3 v1, Vector3 v2)
    {
        float result = (Mathf.Atan2(v2.z - v1.z, v2.x - v1.x) * Mathf.Rad2Deg);
        if (result < 0) result = result + 360;
        if (result > 360) result = result - 360;
        return result;
    }


    public static Vector3 Average(params Vector3[] vectors)
    {
        Vector3 total = Vector3.zero;
        for (int i = 0; i < vectors.Length; i++)
        {
            total += vectors[i];
        }
        return total / vectors.Length;
    }

}
