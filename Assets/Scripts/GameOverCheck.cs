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
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!playedGameOverSE)
        {
            // 地面より下にいたら
            if (transform.position.y < -7)
            {
                isGameOver = true;
                audioSource.PlayOneShot(gameOverSE);
                playedGameOverSE = true;
            }

            // カメラから、負の方向に8くらい離れていたら
            if (transform.position.x - sceneCamera.transform.position.x < -12f)
            {
                isGameOver = true;
                audioSource.PlayOneShot(gameOverSE);
                playedGameOverSE = true;
            }
        }
    }
    
}
