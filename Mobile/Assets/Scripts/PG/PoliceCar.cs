using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PoliceCar : Spawner
{
    static int id = 1;


    public PoliceCar() : base() 
    {
    }

    new
        void Start()
    {
        base.setId(id);
        base.Start();

    }

    new
        void Update()
    {
        base.Update();
    }

}
