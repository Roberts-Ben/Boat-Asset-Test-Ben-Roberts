using System.Collections.Generic;
using UnityEngine;

public class ShipFollowRoute : MonoBehaviour
{
    public List<Route> routes;

    private int activeRoute = 0;
    public List<float> timeToTraverseRoutes;

    private float timePassed = 0;
    private float progress = 0;

    private Vector3 waypointPos;

    public int startingRoute = 0;
    public float startingProgress = 0f;

    public float acceleration = 200f;
    public float maxTurnSpeed = 1f;
    public float turnForce = 0.5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        activeRoute = startingRoute;
        timePassed = startingProgress;
    }

    void FixedUpdate()
    {
        Route currentRoute = routes[activeRoute];

        if (routes.Count == 0 || activeRoute >= routes.Count || routes[activeRoute].waypoints.Count < 4)
        {
            Debug.LogWarning("Invalid route data:" + activeRoute);
            return;
        }

        Vector3 p0 = currentRoute.waypoints[0].position;
        Vector3 p1 = currentRoute.waypoints[1].position;
        Vector3 p2 = currentRoute.waypoints[2].position;
        Vector3 p3 = currentRoute.waypoints[3].position;

        timePassed += Time.deltaTime;
        progress = Mathf.Clamp01(timePassed / timeToTraverseRoutes[activeRoute]);

        // Target is the next step of the curve
        waypointPos = Route.CalculateBezierCurvePoint(progress, p0, p1, p2, p3);

        Vector3 direction = waypointPos - transform.position;

        var localTarget = transform.InverseTransformPoint(waypointPos);
        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        Vector3 eulerAngleVelocity = new (0, angle, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * turnForce * Time.deltaTime);
        
        rb.MoveRotation(rb.rotation * deltaRotation);
        rb.AddForce(direction * acceleration, ForceMode.Force);

        if (progress >= 1)
        {
            timePassed = 0;
            activeRoute++;

            if (activeRoute >= routes.Count)
            {
                activeRoute = 0;
            }
        }
    }
}
