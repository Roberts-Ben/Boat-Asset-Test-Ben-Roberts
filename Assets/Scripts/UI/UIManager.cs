using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private GameObject objectiveTimer;
    [SerializeField] private GameObject inRangeNotif;
    [SerializeField] private GameObject correctOrientationNotif;
    [SerializeField] private GameObject progressUI;
    [SerializeField] private Slider progress;

    [SerializeField] private float timeToCompleteObjective;

    private float currentFill;
    private bool objectiveMet;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (objectiveMet)
        {
            currentFill += (1f / timeToCompleteObjective) * Time.deltaTime;
            progress.value = currentFill;

            if (currentFill >= 1f)
            {
                EndGame();
            }
        }
    }

    public void ResetProgress()
    {
        currentFill = 0;
        progress.value = 0f;
    }

    public void EndGame()
    {
        endGameScreen.SetActive(true);
        objectiveTimer.SetActive(false);
    }

    public void ToggleEndGameScreen(bool isActive)
    {
        endGameScreen.SetActive(isActive);
    }

    public void InsideObjective(bool isInside)
    {
        objectiveTimer.SetActive(isInside);
        objectiveMet = isInside;

        if(!isInside)
        {
            ResetProgress();
        }
    }

    public void UpdateNotifications(bool inRange, bool correctOrientation)
    {
        inRangeNotif.SetActive(inRange);
        correctOrientationNotif.SetActive(correctOrientation);
    }
}