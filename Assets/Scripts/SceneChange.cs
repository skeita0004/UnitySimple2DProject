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

    private float inputLockTimer_ = 0f;
    private const float INPUT_LOCK_TIME = 0.5f;
    private bool isInputLocked_ = false;

    public void ChangeScene()
    {
        SceneManager.LoadScene(scene_);
    }

    void Start()
    {
        isInputLocked_ = true;
        inputLockTimer_ = INPUT_LOCK_TIME;
    }

    void Update()
    {
        if(isInputLocked_)
        {
            inputLockTimer_ -= Time.deltaTime;
            if (inputLockTimer_ < 0 )
            {
                isInputLocked_ = false;
            }
            return;
        }


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
            string currScene = SceneManager.GetActiveScene().name;
            if (currScene == "TitleScene" || 
                currScene == "StageTransition" || 
                currScene == "ResultScene")
            {
                if ( Keyboard.current.anyKey.wasPressedThisFrame ||
                    Gamepad.current != null && Gamepad.current.allControls.Any(controller => controller is ButtonControl button && button.wasPressedThisFrame))
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
            }
        }
    }
}
