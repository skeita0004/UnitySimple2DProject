using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite used;

    private const float GRAVITY_ = -9.8f;
    private float bouncePower_ = 12f;
    private bool isUsed_ = false;
    private Rigidbody2D rb = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( isUsed_ )
        {
            ////Vector2 pos = transform.position;
            ////rb = GetComponent<Rigidbody2D>();
            ////Vector2 vel = rb.linearVelocity;

            //vel.y = (bouncePower_ - GRAVITY_) / Time.deltaTime;
            //rb.linearVelocity = vel;
        }

        // 下からぶつかったら、上に少し跳ねる
        
        // と同時に、アイテムが上方向にひねり出される

        // 跳ね終わったら、空のブロックに代わる。
    }

    private void HitBlock(GameObject _player)
    {
        isUsed_ = true;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if ( _collision.gameObject.CompareTag("Player") )
        {
            foreach ( ContactPoint2D contact in _collision.contacts )
            {
                Rigidbody2D rb = _collision.gameObject.GetComponent<Rigidbody2D>();
                if ( rb != null && rb.linearVelocity.y > 0f)
                {
                    isUsed_ = true;
                    spriteRenderer.sprite = used;
                }
            }
        }
    }
}
