using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    [Header("スクロール速度")]
    public float scrollSpeed = 2.0f;

    [Header("プレイヤーオブジェクト")]
    public GameObject player;

    private bool scrollEnabled_ = false;
    void Update()
    {
        Vector2 playerPos = player.transform.position;

        // playerが、groundより上にいる時、かつ、playerが最初の画面の右端に到達したら、スクロールスタート
        if (scrollEnabled_ == false )
        {
            scrollEnabled_ = (playerPos.y >= -3.5f) && (playerPos.x >= 5.5f);
        }

        if (scrollEnabled_)
        {
            // 毎フレーム、右方向（X軸プラス方向）へ一定速度で移動させる
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);

            if (playerPos.y < -3.5f)
            {
                scrollEnabled_ = false;
            }
        }
    }
}