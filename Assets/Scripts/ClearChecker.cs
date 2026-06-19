using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPole : MonoBehaviour
{
    // 何かがこのセンサー（ポール）に触れた瞬間に実行される固有の関数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 触れてきた相手のタグが "Player" だったら
        if (collision.CompareTag("Player"))
        {
            Debug.Log("ゴールに到達！リザルトシーンへ遷移します。");

            GameManager.isClear = true;
            GameManager.isGameOver = false;

            // ステップ1で作ったシーン名「ResultScene」に切り替える
            SceneManager.LoadScene("ResultScene");
        }
    }
}