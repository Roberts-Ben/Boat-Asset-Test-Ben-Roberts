using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float acceleration = 500f;

    [SerializeField] private float maxTurnSpeed = 2.5f;
    [SerializeField] private float turnForce = 0.5f;
    [SerializeField] private float turnSpeedMult = 0.1f;

    [SerializeField] private float bankForce = 5f;

    [SerializeField] private float baseDrag = 10f;
    [SerializeField] private float maxDrag = 2f;

    private Rigidbody rb;
    public Transform enginePosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply force for acceleration
        if (forwardInput != 0)
        {
            ApplyForwardForce(forwardInput);
        }

        // Apply torque for rotation
        if (horizontalInput != 0)
        {
            ApplyTurningForce(horizontalInput);
        }

        ApplyTiltEffect(horizontalInput);

        // Apply drag
        ApplyWaterResistance();
    }

    private void ApplyForwardForce(float forwardInput)
    {
        Vector3 forceDirection = transform.forward * forwardInput * acceleration;
        rb.AddForceAtPosition(forceDirection, enginePosition.position, ForceMode.Force);
    }

    private void ApplyTurningForce(float horizontalInput)
    {
        float turnSpeed = Mathf.Clamp(rb.velocity.magnitude * turnSpeedMult, 0.5f, maxTurnSpeed);
        rb.AddTorque(transform.up * horizontalInput * turnForce * turnSpeed, ForceMode.Acceleration);
    }

    private void ApplyTiltEffect(float horizontalInput)
    {
        //rb.AddTorque(transform.forward * horizontalInput * bankForce, ForceMode.Acceleration);
        float tiltForce = horizontalInput * bankForce * rb.velocity.magnitude / maxSpeed;
        rb.AddTorque(transform.forward * tiltForce, ForceMode.Acceleration);

        Vector3 stabilizationTorque = -rb.angularVelocity.z * Vector3.forward * 2f;
        rb.AddTorque(stabilizationTorque, ForceMode.Acceleration);
    }

    private void ApplyWaterResistance()
    {
        if (rb.velocity.magnitude > 0.1f) // Only apply resistance if moving
        {
            float speedFactor = rb.velocity.magnitude / maxSpeed;
            float dragForce = Mathf.Lerp(baseDrag, maxDrag, speedFactor); // More drag at higher speeds

            Vector3 resistanceForce = -rb.velocity.normalized * dragForce;
            rb.AddForce(resistanceForce, ForceMode.Force);
        }
    }
}
