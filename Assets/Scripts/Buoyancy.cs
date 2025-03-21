using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Buoyancy : MonoBehaviour
{
    public List<Transform> floatPoints = new();

    public float waterResistance = 0.5f;
    public float waterAngluarDrag = 1f;

    public float buoyancy = 400f;
    public float waterHeight = 0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach(Transform point in floatPoints) // See how much of our boat is below the surface
        {
            float diff = point.position.y - waterHeight;

            if (diff < 0)
            {
                rb.AddForceAtPosition(Vector3.up * buoyancy * Mathf.Abs(diff), point.position, ForceMode.Force); // Push that point back up
            }
        }
    }
}
