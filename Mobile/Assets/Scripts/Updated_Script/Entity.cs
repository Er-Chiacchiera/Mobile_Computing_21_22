using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private float dmg;
    private float fireRate;
    private float maxHealth;
    private float health;
    private float speed;
    private int id;

    protected bool isImmune = false;

    protected float lerpTimer; //serve per la health bar del player

    public Rigidbody2D rigidBody;

    public HealthBar healthBar;

    //Costruttore
    public Entity(float dmg, float fireRate, float maxHealth, float speed)
    {
        this.dmg = dmg;
        this.fireRate = fireRate;
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.speed = speed;

        //healthBar.SetMaxHealth(maxHealth);
        //healthBar.SetHealth(maxHealth);
    }

    public void setHealth (float value) { this.health = value; }
    public float getHealth() { return this.health; }

    public void setDmg(float value) { this.dmg = value; }
    public float getDmg() { return this.dmg; }

    public void setFireRate(float value) { this.fireRate = value; }
    public float getFireRate() { return this.fireRate; }

    public void setMaxHealth(float value) { this.maxHealth = value; }
    public float getMaxHealth() { return this.maxHealth; }

    public void setSpeed(float value) { this.speed = value; }
    public float getSpeed() { return this.speed; }

    public Rigidbody2D getBody() { return this.rigidBody; }

    public void setId(int value) { this.id = value; }
    public int getId() { return id; }

    public void subHealth(float value) { this.health -= value; }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        //se coolide con un proiettile 
        if (collision.gameObject.tag == "Bullet" && this.id != collision.gameObject.GetComponent<Bullet>().GetId() && !isImmune)  
        {
            //danni proiettile
            float currDmg = collision.gameObject.GetComponent<Bullet>().GetDmg();
            this.subHealth(currDmg);
            lerpTimer = 0f;
        }
    }


}
