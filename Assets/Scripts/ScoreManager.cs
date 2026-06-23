using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int STAGE_NUM = 2;
    public static int[] scoreList = new int[STAGE_NUM];
    public static int score;

    public TMP_Text scoreText;

    void Update()
    {
        scoreText.text = "Score : " + score;
    }
}