using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPole : MonoBehaviour
{
    public static int clearNum = 0;

    private bool isTriggered_ = false;
    // 何かがこのセンサー（ポール）に触れた瞬間に実行される固有の関数

    private void Start()
    {
        isTriggered_ = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered_)
        {
            return;
        }

        GetComponent<Collider2D>().enabled = false;

        // 触れてきた相手のタグが "Player" だったら
        if (collision.CompareTag("Player"))
        {
            isTriggered_ = true;

            Debug.Log(clearNum);
            Debug.Log(collision.gameObject.name);

            if (clearNum == 0)
            {
                // クリア回数0回目の時は、ステージ遷移シーンへ（拡張性最悪コード）
                clearNum += 1;
                SceneManager.LoadScene("StageTransition");
            }
            else if (clearNum == 1)
            {
                //Debug.Log("ゴールに到達！リザルトシーンへ遷移します。");

                SceneManager.LoadScene("ResultScene");
            }

        }
    }
}