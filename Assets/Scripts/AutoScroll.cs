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
            if ( GoalPole.clearNum == 0 )
            {
                scrollEnabled_ = (playerPos.y >= -4f) && (playerPos.x >= 5.5f);
            }
            else if ( GoalPole.clearNum == 1 )
            {
                scrollEnabled_ = (playerPos.y >= -4f) && (playerPos.x >= 319.44f);
            }
        }

        if (scrollEnabled_)
        {
            // 毎フレーム、右方向（X軸プラス方向）へ一定速度で移動させる
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);

            if (GameOverCheck.isGameOver)
            {
                scrollEnabled_ = false;
            }
        }
    }
}