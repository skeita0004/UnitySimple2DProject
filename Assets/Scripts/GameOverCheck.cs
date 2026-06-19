using UnityEngine;
using System.Collections;

public class GameOverCheck : MonoBehaviour
{
    public Camera sceneCamera;

    public AudioClip gameOverSE;
   

    public static bool isGameOver = false;


    private AudioSource audioSource;

    private bool playedGameOverSE = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!playedGameOverSE)
        {
            // 地面より下にいたら
            if (transform.position.y < -7)
            {
                GameManager.isClear = false;
                GameManager.isGameOver = true;

                isGameOver = true;
                
            }

            // カメラから、負の方向に8くらい離れていたら
            if (transform.position.x - sceneCamera.transform.position.x < -12f)
            {
                GameManager.isClear = false;
                GameManager.isGameOver = true;

                isGameOver = true;
                
               
            }
        }
    }
    
}
