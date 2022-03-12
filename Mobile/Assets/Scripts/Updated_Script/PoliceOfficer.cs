using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficer : Shooter
{
    static int id = 2;

    public PoliceOfficer() : base(10, 0.6f, 100, 1.5f)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        base.setId(id);
        target = GameObject.Find("Robot");
        targetPosition = target.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (base.getHealth() <= 0)
        {
            //this.isDead = true;
            Destroy(gameObject);
        }

        base.setXDirection(base.getTargetPosition().position.x - base.getBody().position.x);
        base.setYDirection(base.getTargetPosition().position.y - base.getBody().position.y);
        base.setAngle(Mathf.Atan2(base.getDirection().y, base.getDirection().x) * Mathf.Rad2Deg);
    }

    private void FixedUpdate()
    {
        base.getBody().rotation = base.getAngle() ;

        if (Time.time > (1f / base.getFireRate()) + base.getLastShoot())
        {
            foreach (Transform firePoint in this.firePoints)
                base.Shoot(firePoint);
        }

    }
}
