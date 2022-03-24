using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficer : Shooter
{
    static int id = 2;

    public PoliceOfficer() : base()
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
