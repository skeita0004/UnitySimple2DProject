using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public GameObject boxItem;

    SpriteRenderer spriteRenderer;
    public Sprite used;

    

    private Vector2 itemMovePoint;

    private bool isUsed_ = false;

    private BoxCollider2D[] colliders_;
    private CapsuleCollider2D playerCollider_;

    private AudioSource audioSource;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerCollider_ = player.GetComponent<CapsuleCollider2D>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        colliders_ = GetComponentsInChildren<BoxCollider2D>();

    


        itemMovePoint = (Vector2)transform.position + new Vector2(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if ( isUsed_ )
        {
            // アイテムがにゅる～って出てくる
            if (boxItem != null )
            { 
                boxItem.transform.position = Vector2.Lerp(boxItem.transform.position, itemMovePoint, 0.05f);
               
               
            }
        }
    }

    private void HitBlock(GameObject _player)
    {
        isUsed_ = true;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (isUsed_ == false)
        {
            if ( colliders_[1].IsTouching(playerCollider_) && _collision.gameObject.CompareTag("Player") )
            {
                isUsed_ = true;
                spriteRenderer.sprite = used;

                boxItem = Instantiate(boxItem);
                boxItem.transform.position = transform.position;
                //boxItem.transform.SetParent(gameObject.transform);
                //boxItem.gameObject.SetActive(false);
            }
        }
    }
}
