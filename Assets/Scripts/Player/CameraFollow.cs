using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;

    public Vector3 cameraOffset = new();
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("No camera target");
            return;
        }

        Vector3 targetPosition = target.TransformPoint(cameraOffset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);
    }
}
