using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MilitaryJeep : Spawner
{
    static int id = 4;
    [SerializeField]
    private GameObject explosion;

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

    private new void OnDestroy()
    {
        if (getHealth() <= 0)
            GameObject.Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.5f);
    }

}
