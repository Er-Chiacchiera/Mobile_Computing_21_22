using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MilitaryJeep : Spawner
{
    static int id = 4;


    public MilitaryJeep() : base()
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
