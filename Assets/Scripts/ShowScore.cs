using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    int counter = 0;

    void Start()
    {
        
    }


    void Update()
    {
        if (GameOverCheck.isGameOver == false)
        {
            scoreText.alignment = TextAlignmentOptions.Center;
            scoreText.text = "Score:" + ScoreManager.score;
            counter++;

            if (counter % 20 == 0)
            {
                if ((int)TimerManager.timeLimit != 0)
                {
                    TimerManager.timeLimit -= 1f;
                    ScoreManager.score += 10;
                }
            }
        }
        else
        {
            scoreText.fontSize = 12;
            scoreText.text = "Score: Not because you died";
        }
    }
}
