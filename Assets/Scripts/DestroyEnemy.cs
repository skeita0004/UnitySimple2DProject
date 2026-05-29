using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (transform.position.y > collision.gameObject.transform.position.y)
            {
                //if (PlayerController.playerIsGrounded == false)
                {
                    this.GetComponent<Rigidbody2D>().linearVelocityX += 2f;
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
