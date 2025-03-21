using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;

    public Vector3 cameraOffset = new();
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(cameraOffset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(target.position);
    }
}
