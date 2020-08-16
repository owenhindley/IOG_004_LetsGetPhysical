using UnityEngine;

public class GizmosExtension {

    public static void DrawRect(Rect rect, Transform relativeTo= null)
    {
        if (relativeTo == null)
        {
            Gizmos.DrawLine(
                new Vector3(rect.x,rect.y,0),
                new Vector3(rect.x + rect.width, rect.y, 0)
            );
            Gizmos.DrawLine(
                new Vector3(rect.x, rect.y, 0),
                new Vector3(rect.x, rect.y+rect.height, 0)
            );
            Gizmos.DrawLine(
                new Vector3(rect.x+rect.width, rect.y + rect.height, 0),
                new Vector3(rect.x, rect.y + rect.height, 0)
            );
            Gizmos.DrawLine(
                new Vector3(rect.x + rect.width, rect.y + rect.height, 0),
                new Vector3(rect.x + rect.width, rect.y, 0)
            );
        }
        else
        {
            Gizmos.DrawLine(
                relativeTo.position + relativeTo.TransformVector(new Vector3(-rect.width / 2, -rect.height / 2, 0)),
                relativeTo.position + relativeTo.TransformVector(new Vector3(rect.width / 2, -rect.height / 2, 0))
            );
            Gizmos.DrawLine(
                relativeTo.position + relativeTo.TransformVector(new Vector3(-rect.width / 2, -rect.height / 2, 0)),
                relativeTo.position + relativeTo.TransformVector(new Vector3(-rect.width / 2, rect.height/2,0))
            );
            Gizmos.DrawLine(
                relativeTo.position + relativeTo.TransformVector(new Vector3(rect.width / 2, -rect.height/ 2, 0)),
                relativeTo.position + relativeTo.TransformVector(new Vector3(rect.width / 2, rect.height/2,0))
            );
            Gizmos.DrawLine(
                relativeTo.position + relativeTo.TransformVector(new Vector3(-rect.width / 2, rect.height/2,0)),
                relativeTo.position + relativeTo.TransformVector(new Vector3(rect.width / 2, rect.height/2,0))
            );
        }
    }

    public static void DrawPoly(params Vector3[] points)
    {
        for (int i = 0; i < points.Length-1; i++)
        {
            Gizmos.DrawLine(points[i], points[i+1]);
        }
        Gizmos.DrawLine(points[points.Length-1], points[0]);

    }

    public static void DrawCircleXZ(Vector3 position, float radius, float dashLength = 1f, int resolution =32)
    {
        for (int i = 0; i < resolution; i++)
        {
            Gizmos.DrawLine(position + Quaternion.Euler(0,i/(float)resolution * 360,0) * (Vector3.forward * radius),
                position + Quaternion.Euler(0,(i+dashLength)/(float)resolution *  360,0) * (Vector3.forward * radius));
        }
    }

    public static void DrawPoint(Vector3 position, float size =1)
    {
        Gizmos.DrawLine(position - Vector3.left * size, position  + Vector3.left * size);
        Gizmos.DrawLine(position - Vector3.up * size, position + Vector3.up * size);
        Gizmos.DrawLine(position - Vector3.forward * size, position + Vector3.forward * size);
    }
    
    public static void DrawTransformWireCube(Transform transform)
    {
        Gizmos.DrawLine(transform.TransformPoint((Vector3.left + Vector3.up + Vector3.forward)/2f), 
                transform.TransformPoint((Vector3.left + Vector3.up - Vector3.forward)/2f));
        Gizmos.DrawLine(transform.TransformPoint((Vector3.left - Vector3.up + Vector3.forward)/2f), 
            transform.TransformPoint((Vector3.left - Vector3.up - Vector3.forward)/2f));
        
        Gizmos.DrawLine(transform.TransformPoint((-Vector3.left + Vector3.up + Vector3.forward)/2f), 
            transform.TransformPoint((-Vector3.left + Vector3.up - Vector3.forward)/2f));
        
        Gizmos.DrawLine(transform.TransformPoint((-Vector3.left - Vector3.up + Vector3.forward)/2f), 
            transform.TransformPoint((-Vector3.left - Vector3.up - Vector3.forward)/2f));
        
        Gizmos.DrawLine(transform.TransformPoint((Vector3.left + Vector3.up + Vector3.forward)/2f), 
            transform.TransformPoint((Vector3.left - Vector3.up + Vector3.forward)/2f));

        Gizmos.DrawLine(transform.TransformPoint((-Vector3.left + Vector3.up + Vector3.forward)/2f), 
            transform.TransformPoint((-Vector3.left - Vector3.up + Vector3.forward)/2f));
        
        Gizmos.DrawLine(transform.TransformPoint((Vector3.left + Vector3.up - Vector3.forward)/2f), 
            transform.TransformPoint((Vector3.left - Vector3.up - Vector3.forward)/2f));

        Gizmos.DrawLine(transform.TransformPoint((-Vector3.left + Vector3.up - Vector3.forward)/2f), 
            transform.TransformPoint((-Vector3.left - Vector3.up - Vector3.forward)/2f));
        
        
        Gizmos.DrawLine(transform.TransformPoint((-Vector3.left + Vector3.up - Vector3.forward)/2f), 
            transform.TransformPoint((Vector3.left + Vector3.up - Vector3.forward)/2f));
        
        Gizmos.DrawLine(transform.TransformPoint((-Vector3.left + Vector3.up + Vector3.forward)/2f), 
            transform.TransformPoint((Vector3.left + Vector3.up + Vector3.forward)/2f));

        Gizmos.DrawLine(transform.TransformPoint((-Vector3.left - Vector3.up - Vector3.forward)/2f), 
            transform.TransformPoint((Vector3.left - Vector3.up - Vector3.forward)/2f));
        
        Gizmos.DrawLine(transform.TransformPoint((-Vector3.left - Vector3.up + Vector3.forward)/2f), 
            transform.TransformPoint((Vector3.left - Vector3.up + Vector3.forward)/2f));
    }
    
    
}
