using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : Enemy
{

    protected GameObject target;
    protected Transform targetPosition;
    private Vector2 direction;
    private float angle;
    public List<Transform> firePoints;
    public GameObject bulletPrefab;


    private float lastShot;


    public Shooter(float dmg, float fireRate, float maxHealth, float speed) : base (dmg, fireRate, maxHealth, speed)
    {
        
        lastShot = Time.time;
    }

    public void setXDirection(float value) { this.direction.x = value;}
    public void setYDirection(float value) { this.direction.y = value; }
    public Vector2 getDirection() { return this.direction; }

    public float getLastShoot() { return this.lastShot; }

    public void setAngle(float value) { this.angle = value; }
    public float getAngle() { return this.angle; }

    public Transform getTargetPosition() { return this.targetPosition;}

    protected void Shoot(Transform firePoint)
    {
        animator.SetTrigger("Shoot");
        lastShot = Time.time;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //setting danno proiettile
        bullet.GetComponent<Bullet>().SetDmg(base.getDmg()); //proprietaria della classe bullet
        bullet.GetComponent<Bullet>().SetId(base.getId());
        rb.AddForce(firePoint.up * bullet.GetComponent<Bullet>().bulletVelocity, ForceMode2D.Impulse);
        Debug.Log(base.getId());
    }
}
