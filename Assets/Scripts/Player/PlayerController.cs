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
        float horizontalnput = Input.GetAxis("Horizontal");

        // Apply force for acceleration
        if (forwardInput != 0)
        {
            rb.AddForce(transform.forward * forwardInput * acceleration, ForceMode.Acceleration);
        }
        // Apply torque for rotation
        if (horizontalnput != 0)
        {
            rb.AddTorque(transform.up * horizontalnput * (turnForce * rb.velocity.magnitude / acceleration), ForceMode.Acceleration);
        }

        // Apply drag
        if (rb.velocity.magnitude > 0.1f)
        {
            rb.AddForce(-rb.velocity.normalized * deceleration, ForceMode.Acceleration);
        }

        // Cap speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
