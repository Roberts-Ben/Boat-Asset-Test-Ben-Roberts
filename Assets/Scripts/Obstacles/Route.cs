using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public List<Transform> waypoints;

    private Vector3 gizmoPos;

    private void OnDrawGizmos()
    {
        for(int i = 0; i < waypoints.Count; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(waypoints[i].position, Vector3.one);
        }

        for (float time = 0; time <= 1; time += 0.05f)
        {
            // Drop visuals at intervals along the path of the curve
            gizmoPos = Mathf.Pow(1 - time, 3) * waypoints[0].position + 
                3 * Mathf.Pow(1 - time, 2) * time * waypoints[1].position + 
                3 * (1 - time) * Mathf.Pow(time, 2) * waypoints[2].position + 
                Mathf.Pow(time, 3) * waypoints[3].position;
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(gizmoPos, 0.25f);
        }

        Gizmos.DrawLine(new Vector3(waypoints[0].position.x, waypoints[0].position.y, waypoints[0].position.z), // Draw lines between start/end node and it's director
            new Vector3(waypoints[1].position.x, waypoints[1].position.y, waypoints[1].position.z));
        Gizmos.DrawLine(new Vector3(waypoints[2].position.x, waypoints[2].position.y, waypoints[2].position.z), 
            new Vector3(waypoints[3].position.x, waypoints[3].position.y, waypoints[3].position.z));

    }
}
