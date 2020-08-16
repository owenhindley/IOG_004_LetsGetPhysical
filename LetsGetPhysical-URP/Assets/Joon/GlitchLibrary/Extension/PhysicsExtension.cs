using UnityEngine;

public static class PhysicsExtension
{
    public static bool Raycast(Vector3 startPosition, Vector3 endPosition, out RaycastHit hit, int layerMask)
    {
        var direction = (endPosition - startPosition).normalized;
        var distance = Vector3.Distance(startPosition, endPosition);
        return Physics.Raycast(startPosition, direction, out hit, distance, layerMask);
    }
}