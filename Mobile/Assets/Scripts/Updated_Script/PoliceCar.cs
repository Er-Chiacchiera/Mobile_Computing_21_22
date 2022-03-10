using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : Enemy
{
    static int id = 1;

    public PoliceCar() : base(0, 7, 100, 1.5f) 
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        base.setId(id);
    }

    // Update is called once per frame
    void Update()
    {
        if (base.getHealth() <= 0)
        {
            //this.isDead = true;
            Destroy(gameObject);
        }
    }
}
