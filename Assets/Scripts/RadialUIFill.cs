using UnityEngine;
using UnityEngine.UI;

public class RadialUIFill : MonoBehaviour
{
    private float currentFill = 0;
    public int timeToFill;

    public Slider progress;
    public bool ObjectiveMet { get; set; }

    void Update()
    {
        if (ObjectiveMet)
        {
            currentFill += (1f / timeToFill) * Time.deltaTime;
            progress.value = currentFill;

            if(currentFill >= 1f)
            {
                if(!UIManager.instance.gameEnded)
                {
                    UIManager.instance.EndGame();
                }
            }
        }
    }

    public void ResetProgress()
    {
        currentFill = 0;
    }
}
