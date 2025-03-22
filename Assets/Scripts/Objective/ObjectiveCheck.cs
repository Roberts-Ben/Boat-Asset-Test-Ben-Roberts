using UnityEngine;
using System.Collections.Generic;

public class ObjectiveCheck : MonoBehaviour
{
    private Transform objective;
    private Transform playerShip;

    private bool positionedCorrectly = false;
    private bool orientedCorrectly = false;
    private bool insideObjective = false;

    public float positionTolerance = 2f;
    public float rotationTolerance = 10f;

    private float notificationTimer = 0f;

    private void Awake()
    {
        objective = transform;
    }

    private void Update()
    {
        if (!UIManager.instance.gameEnded && insideObjective)
        {
            bool objectiveMet = positionedCorrectly && orientedCorrectly;

            UIManager.instance.InsideObjective(objectiveMet);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerShip != null)
        {
            CheckPosition();
        }
        else
        {
            Debug.LogWarning("Player ship not found by " + transform.name);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        playerShip = other.transform;
        insideObjective = true;
    }
    private void OnTriggerExit(Collider other)
    {
        insideObjective = false;
        ForceSetNotificationState(false);
    }

    private void CheckPosition()
    {
        positionedCorrectly = Vector3.Distance(playerShip.position, objective.position) < positionTolerance; // Ensure the player is close enough to the dock

        float angle = Vector3.SignedAngle(playerShip.forward, objective.forward, Vector3.up);
        orientedCorrectly = Mathf.Abs(angle) <= rotationTolerance; // Ensure they are facing the right way

        notificationTimer += Time.deltaTime;
        if (notificationTimer >= 1f)
        {
            notificationTimer = 0f;
            ToggleNotificationState();
        } 
    }

    private void ToggleNotificationState()
    {
        UIManager.instance.tooFarNotif.SetActive(!positionedCorrectly);
        UIManager.instance.wrongOrientationNotif.SetActive(!orientedCorrectly);
    }

    private void ForceSetNotificationState(bool isEnabled) // Used to force notifications on or off instantly when entering or leaving the trigger
    {
        UIManager.instance.tooFarNotif.SetActive(isEnabled);
        UIManager.instance.wrongOrientationNotif.SetActive(isEnabled);
    }
}
