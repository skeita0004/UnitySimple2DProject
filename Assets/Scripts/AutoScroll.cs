using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    [Header("スクロール速度")]
    public float scrollSpeed = 2.0f;

    void Update()
    {
        // 毎フレーム、右方向（X軸プラス方向）へ一定速度で移動させる
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
    }
}