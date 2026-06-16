using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 3.0f;
    private int dir_ = -1;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkDist;

    private bool isMove_ = false;
    private GameObject player_ = null;

    void Start()
    {
        groundCheck = transform.GetChild(0).transform;

        player_ = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float toPlayerDist = Vector2.Distance(transform.position, player_.transform.position);
        
        if (toPlayerDist < 10f)
        {
            isMove_ = true;
        }

        if (isMove_)
        {
            Vector3 pos = this.transform.position;
            pos.x += (speed * Time.deltaTime) * dir_;
            this.transform.position = pos;

            if (toPlayerDist > 15f)
            {
                isMove_ = false;
            }
        }

        bool isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDist, groundLayer);

        if ( isGroundAhead == false )
        {
            Turn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Turn();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y < transform.position.y)
            {
                GameOverCheck.isGameOver = true;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Turn();
        }
    }

    private void Turn()
    {
        dir_ *= -1;
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
