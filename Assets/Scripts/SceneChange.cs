using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] public string scene_;

    private float timer_ = 0f;
    private const float SCENE_CHANGE_TIME = 1.5f;

    public static bool isSceneChage = false;

    public void ChangeScene()
    {
        SceneManager.LoadScene(scene_);
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (isSceneChage)
        {
            timer_ += Time.deltaTime;

            // シーン切り替え時、1.5秒待機(演出のため)
            if (timer_ > SCENE_CHANGE_TIME)
            {
                isSceneChage = false;
                ChangeScene();
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "TitleScene")
            {
                if ( Keyboard.current.anyKey.isPressed ||
                    Gamepad.current != null && Gamepad.current.allControls.Any(controller => controller is ButtonControl button && button.IsPressed()))
                {
                    isSceneChage = true;
                }
            }
            else if (SceneManager.GetActiveScene().name == "PlayScene")
            {
                if (GameOverCheck.isGameOver)
                {
                    isSceneChage = true;
                }
                // ステージが2の時はResultへ、
                // ステージが1の時はステージ遷移シーンへ。
            }
        }
    }

}
