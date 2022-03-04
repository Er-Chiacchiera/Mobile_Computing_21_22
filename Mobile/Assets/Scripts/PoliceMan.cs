using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMan : MonoBehaviour
{
    private float dmg = 10;
    private float fireRate = 1;
    private float healthMax = 100;
    private float health;
    void Start()
    {
        health = healthMax;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //se la collisione è con un proiettile del robot
        if (collision.gameObject.tag == "Bullet")
        {
            //danni proiettile
            float dmg = collision.gameObject.GetComponent<Bullet>().GetDmg();
            health -= dmg;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public float GetDmg()
    {
        return dmg;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

}
