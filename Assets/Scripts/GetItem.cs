using System;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    public AudioClip getItemSE;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        
        if (_collision.gameObject.CompareTag("Item"))
        {
            // SE再生
            AudioSource.PlayClipAtPoint(getItemSE, transform.position,2.0f);

            Destroy(_collision.gameObject);
            ScoreManager.score += 100;
        }
    }
}
