using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : MonoBehaviour
{

    private BoxCollider2D boxcollider;
    private Vector2 position;
    private bool colliso;

    // Start is called before the first frame update
    private void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
        colliso = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movimento verso il basso
        if (!colliso)
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.04f);
    }
}
