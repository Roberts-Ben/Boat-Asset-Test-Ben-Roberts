using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public float acceleration = 5f;
    public float turnForce = 0.2f;
    public float deceleration = 1f;

    public float maxSpeed = 20f;
    public float maxTurnSpeed = 1f;

    private Rigidbody rb;

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
            rb.AddForce(transform.forward * forwardInput * acceleration, ForceMode.Acceleration);
        }

        // Apply torque for rotation
        if (horizontalInput != 0)
        {
            rb.AddTorque(transform.up * horizontalInput * (turnForce * rb.velocity.magnitude / acceleration), ForceMode.Acceleration);
        }

        // Apply drag
        if (forwardInput == 0)
        {
            rb.AddForce(-rb.velocity.normalized * deceleration, ForceMode.Acceleration);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
