using System.Collections.Generic;
using UnityEngine;

public class ShipFollowRoute : MonoBehaviour
{
    public List<Transform> routes;

    private int activeRoute = 0;
    public float timeToTraverseRoute = 60f;

    private float timePassed = 0;
    private float progress = 0;

    private Vector3 waypointPos;

    void FixedUpdate()
    {
        Vector3 p0 = routes[activeRoute].GetChild(0).position;
        Vector3 p1 = routes[activeRoute].GetChild(1).position;
        Vector3 p2 = routes[activeRoute].GetChild(2).position;
        Vector3 p3 = routes[activeRoute].GetChild(3).position;

        timePassed += (1f / timeToTraverseRoute) * Time.deltaTime; // Slowly progress through the current route
        progress = timePassed % timeToTraverseRoute;

        // Target is the next step of the curve
        waypointPos = Mathf.Pow(1 - progress, 3) * p0 + 3
            * Mathf.Pow(1 - progress, 2) * progress * p1 + 3
            * (1 - progress) * Mathf.Pow(progress, 2) * p2 + Mathf.Pow(progress, 3) * p3;

        transform.LookAt(waypointPos);
        transform.position = waypointPos;

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
