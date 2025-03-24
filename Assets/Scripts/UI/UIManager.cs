using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject endGameScreen;
    public GameObject objectiveTimer;
    public GameObject tooFarNotif;
    public GameObject wrongOrientationNotif;

    public bool gameEnded = false;

    public RadialUIFill objectiveRadialUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        endGameScreen.SetActive(true);
        objectiveTimer.SetActive(false);
    }
    public void ToggleEndGameScreen(bool active)
    {
        endGameScreen.SetActive(active);
    }

    public void InsideObjective(bool insideObjective)
    {
        objectiveTimer.SetActive(insideObjective);
        objectiveRadialUI.ObjectiveMet = insideObjective;

        if(!insideObjective)
        {
            objectiveRadialUI.ResetProgress();
        }
    }
}
