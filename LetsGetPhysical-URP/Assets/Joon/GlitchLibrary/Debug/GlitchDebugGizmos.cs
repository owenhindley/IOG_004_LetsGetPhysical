using UnityEngine;

public class GlitchDebugGizmos : MonoBehaviour {

    public bool showSphere;
    public bool showWireSphere;
    public bool showWireCube;
    public bool showForwardDirection;
    public Color color = Color.green;
    public Color colorDeselected = Color.green;
    public float scale=0.1f;
    public bool connectAllChildren;
    public bool useTransformScale;
    
    void Draw()
    {
        if (connectAllChildren)
        {
            var list = transform.GetChildrenAsTransforms();
            for (int i = 0; i < list.Length; i++)
            {
                DrawAt(list[i].position);
                if(i < list.Length - 1)
                    Gizmos.DrawLine(list[i].position , list[i+1].position);
            }
        }
        else
        {
            DrawAt(transform.position);
        }
    }

    void DrawAt(Vector3 position)
    {
        var scale = this.scale;
        if (useTransformScale) scale = transform.lossyScale.x;
        if (showSphere) Gizmos.DrawSphere(position, scale);
        if (showWireSphere) Gizmos.DrawWireSphere(position, scale);
        //if (showWireCube) Gizmos.DrawWireCube(position, Vector3.one * scale);

        if (showWireCube) GizmosExtension.DrawTransformWireCube(transform);
        
        Gizmos.matrix = Matrix4x4.LookAt(transform.position, transform.position + transform.forward, transform.up);
        if (showForwardDirection) Gizmos.DrawFrustum(Vector3.zero, 30, 0, scale, 1);
        Gizmos.matrix = Matrix4x4.identity;
        
        
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = colorDeselected;
        Draw();
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Draw();
    }
}
