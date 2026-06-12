using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    public Camera sceneCamera;

    public static bool isGameOver = false;

    void Start()
    {
        
    }

    void Update()
    {
        // 地面より下にいたら
        if (transform.position.y < -7)
        {
            isGameOver = true;
        }

        // カメラから、負の方向に8くらい離れていたら
        if (transform.position.x - sceneCamera.transform.position.x < -12f)
        {
            isGameOver = true;
        }
    }
}
