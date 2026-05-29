using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 3.0f;
    private int dir_ = -1;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.x += (speed * Time.deltaTime) * dir_;
        this.transform.position = pos;
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
    }
}
