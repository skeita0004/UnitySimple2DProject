using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 3.0f;
    private int dir_ = -1;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkDist;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.x += (speed * Time.deltaTime) * dir_;
        this.transform.position = pos;

        //bool isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDist, groundLayer);

        //if (isGroundAhead == false)
        //{
        //    dir_ *= -1;
        //    Vector3 scale = this.transform.localScale;
        //    scale.x *= -1;
        //    transform.localScale = scale;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            dir_ *= -1;
            Vector3 scale = this.transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y < transform.position.y)
            {
                GameOverCheck.isGameOver = true;
            }
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawLine(
    //        groundCheck.position,
    //        groundCheck.position + Vector3.down * checkDist
    //    );
    //}

}
