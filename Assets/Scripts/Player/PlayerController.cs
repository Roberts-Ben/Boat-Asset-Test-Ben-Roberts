using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 500f;
    public float deceleration = 0.98f;

    public float maxTurnSpeed = 2.5f;
    public float turnForce = 0.5f;
    public float turnSpeedMult = 0.1f;

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

        // Apply drag
        if (forwardInput == 0)
        {
            rb.AddForce(-rb.velocity.normalized * deceleration, ForceMode.Acceleration);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void ApplyForwardForce(float forwardInput)
    {
        //rb.AddForce(transform.forward * forwardInput * acceleration, ForceMode.Acceleration); 
        Vector3 forceDirection = transform.forward * forwardInput * acceleration;
        rb.AddForceAtPosition(forceDirection, enginePosition.position, ForceMode.Force);
    }

    private void ApplyTurningForce(float horizontalInput)
    {
        //rb.AddTorque(transform.up * horizontalInput * (turnForce * rb.velocity.magnitude / acceleration), ForceMode.Acceleration);
        float turnSpeed = Mathf.Clamp(rb.velocity.magnitude * turnSpeedMult, 0.5f, maxTurnSpeed);
        rb.AddTorque(transform.up * horizontalInput * turnForce * turnSpeed, ForceMode.Acceleration);
    }
}
