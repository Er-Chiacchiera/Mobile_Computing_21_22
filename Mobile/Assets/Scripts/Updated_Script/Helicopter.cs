using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : Shooter
{
    static int id = 3;

    public Helicopter() : base(10, 1, 100, 1.5f) //parametri da sistemare
    {
    }

    new

        // Start is called before the first frame update
        void Start()
    {
        base.setId(id);
        base.Start();
    }

    new

        // Update is called once per frame
        void Update()
    {
        base.Update();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
    }


}
