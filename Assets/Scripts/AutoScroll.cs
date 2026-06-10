using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    [Header("スクロール速度")]
    public float scrollSpeed = 2.0f;

    void Update()
    {
        // playerが、groundより上にいる時、かつ、playerが、最初の画面の右端に到達したら、スクロールスタート
        if ( true /**/)
        {
            // 毎フレーム、右方向（X軸プラス方向）へ一定速度で移動させる
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }
    }
}