using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    
    private float time_ = 0f;
    private const float NORMAL_BLINK_SEC = 1f;
    private const float FAST_BLINK_SEC = NORMAL_BLINK_SEC / 16f;


    void Start()
    {
        
    }

    void BlinkOperation(float _blinkIntervalSec)
    {
        if ( time_ > _blinkIntervalSec )
        {
            ChangeAlpha(0f);
        }
        if ( time_ > _blinkIntervalSec * 2)
        {
            ChangeAlpha(1f);
            time_ = 0f;
        }
    }

    void ChangeAlpha(float _alphaValue)
    {
        Color color = spriteRenderer.color;
        color.a = _alphaValue;
        spriteRenderer.color = color;
    }

    void FixedUpdate()
    {
        time_ += Time.deltaTime;

        if ( SceneChange.isSceneChage )
        {
            // シーン切り替え時に高速で点滅する
            BlinkOperation(FAST_BLINK_SEC);
        }
        else
        {
            BlinkOperation(NORMAL_BLINK_SEC);
        }
    }
}
