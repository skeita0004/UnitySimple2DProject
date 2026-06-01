using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeLimit = 60f;

    void Update()
    {
        timeLimit -= Time.deltaTime;

        if (timeLimit < 0)
        {
            timeLimit = 0;
        }

        timerText.text = "Time : " + Mathf.CeilToInt(timeLimit);

        if (timeLimit <= 0)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}