using UnityEngine;

public class SimpleConstraints : MonoBehaviour
{
    public Transform targetPosition;
    public Vector3 positionWeights = Vector3.one;

    public Transform targetRotation;

    public Transform directionFrom;
    public Transform directionTo;

    public bool useFromToDirection;

    public Vector3 rotationWeights;

    private Vector3 startPosition;
    //private Quaternion startRotation;

    void Start()
    {
        //startRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (targetPosition != null)
        {
            transform.position = Vector3.Scale(targetPosition.position, positionWeights);
        }

        if (targetRotation != null)

        {
            if (useFromToDirection)
            {
                transform.LookAt2D(transform.position + (directionTo.position - directionFrom.position).normalized, Vector3.up, 0.1f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.Scale(targetRotation.eulerAngles, rotationWeights));
            }
        }
    }
}
