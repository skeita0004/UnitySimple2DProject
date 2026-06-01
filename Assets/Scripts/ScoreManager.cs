using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;

    public TMP_Text scoreText;

    void Update()
    {
        scoreText.text = "Score : " + score;
    }
}