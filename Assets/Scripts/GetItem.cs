using System;
using UnityEngine;

public class GetItem : MonoBehaviour
{
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
            Destroy(_collision.gameObject);
            ScoreManager.score += 100;
        }
    }
}
