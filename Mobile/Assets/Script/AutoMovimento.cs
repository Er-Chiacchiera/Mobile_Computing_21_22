using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovimento : MonoBehaviour
{
    private RaycastHit2D hit;
    private BoxCollider2D boxCollider;
    private Vector2 position;
    // Start is called before the first frame update
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //controllo se ho l'asse y bloccata
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, 0.04f), Mathf.Abs(0.04f * Time.deltaTime), LayerMask.GetMask("Enemy", "Robot"));
        if (hit.collider == null)
        {
            //allora mi posso muovere
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.04f);
        }
    }
}
