using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string _scene;
    public void ChangeScene()
    {
        SceneManager.LoadScene(_scene);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if ( Keyboard.current.anyKey.isPressed )
        {
            ChangeScene();
        }
    }

}
