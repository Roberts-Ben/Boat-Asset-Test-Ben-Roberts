using UnityEngine;

public class ObjectiveCheck : MonoBehaviour
{
    [SerializeField] private Transform objective;
    [SerializeField] private float positionTolerance = 2f;
    [SerializeField] private float rotationTolerance = 10f;

    private Transform playerShip;

    private bool positionedCorrectly;
    private bool orientedCorrectly;
    private bool prevPositionState;
    private bool prevOrientationState;

    private bool stateChanged;

    private void Awake()
    {
        objective = this.transform;
    }

    private void FixedUpdate()
    {
        bool objectiveMet = positionedCorrectly && orientedCorrectly;

        if (stateChanged)
        {
            UIManager.instance.InsideObjective(objectiveMet);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPositionAndOrientation();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerShip = other.transform;
            UIManager.instance.UpdateNotifications(false, false);
            CheckPositionAndOrientation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.UpdateNotifications(false, false); // Reset notifications
        }
    }

    private void CheckPositionAndOrientation()
    {
        if (playerShip == null)
        {
            return;
        }

        prevPositionState = positionedCorrectly;
        prevOrientationState = orientedCorrectly;

        float distance = Vector3.Distance(playerShip.position, objective.position);
        positionedCorrectly = distance < positionTolerance;

        float angle = Vector3.SignedAngle(playerShip.forward, objective.forward, Vector3.up);
        orientedCorrectly = Mathf.Abs(angle) <= rotationTolerance;

        stateChanged = (prevPositionState != positionedCorrectly || prevOrientationState != orientedCorrectly);
        UIManager.instance.UpdateNotifications(!positionedCorrectly, !orientedCorrectly);
    }
}