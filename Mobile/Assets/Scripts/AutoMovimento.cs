using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovimento : MonoBehaviour
{
    //private RaycastHit2D hit;
    //private BoxCollider2D boxCollider;
    private bool colliso = false;
    
    private void Start()
    {
        //boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        colliso = true;
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        colliso = false;
    }
    
    private void FixedUpdate()
    {
        //if (!colliso)
            //allora mi posso muovere
            //transform.position = new Vector2(transform.position.x, transform.position.y - (0.4f * Time.deltaTime));
    }
}
