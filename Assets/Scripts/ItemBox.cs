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

    private BoxCollider2D[] colliders_;
    private BoxCollider2D playerCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<BoxCollider2D>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        colliders_ = GetComponentsInChildren<BoxCollider2D>();
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
        Debug.Log("あたった");
        if ( colliders_[1].IsTouching(playerCollider) && _collision.gameObject.CompareTag("Player") )
        {
                isUsed_ = true;
                spriteRenderer.sprite = used;
        }
    }
}
