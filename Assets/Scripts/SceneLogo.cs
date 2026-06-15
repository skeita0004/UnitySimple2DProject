using UnityEngine;
using UnityEngine.UI;

public class SceneLogo : MonoBehaviour
{
    public Image sceneLogo;

    public Sprite clearLogo;
    public Sprite overLogo;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameOverCheck.isGameOver) // Result.isClear
        {
            sceneLogo.sprite = overLogo;
        }
        else
        {
            sceneLogo.sprite = clearLogo;
        }
    }
}
