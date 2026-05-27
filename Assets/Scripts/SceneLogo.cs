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
        if (true) // Result.isClear
        {
            sceneLogo.sprite = clearLogo;
        }
        else
        {
            sceneLogo.sprite = overLogo;
        }
    }
}
