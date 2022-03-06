using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private float dmg = 10;
    private float fireRate = 7;
    private float maxHealth = 100;
    private float health;

    public HealthBar healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
    }

    public float GetDmg()
    {
        return dmg;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public void RestoreHp20()
    {
        health += maxHealth * 0.2f;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.SetHealth(health);
    }

    public bool fullHealth()
    {
        return health == maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //se la collisione è con un proiettile nemico
        if (collision.gameObject.tag == "BulletEnemy")
        {
            //danni proiettile
            float dmg = collision.gameObject.GetComponent<BulletEnemy>().GetDmg();
            health -= dmg;
            healthBar.SetHealth(health);
            //if (health <= 0) Destroy(gameObject);
        }
    }
}
