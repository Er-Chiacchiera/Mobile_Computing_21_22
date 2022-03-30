using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float dmg;
    [SerializeField] private float fireRate;
    [SerializeField] private float maxHealth;
    private float health;
    [SerializeField] private float speed;
    private int id;

    protected float lerpTimer; //serve per la health bar del player

    public Rigidbody2D rigidBody;

    //public HealthBar healthBar;

    public void Start()
    {
        health = maxHealth;
    }


    //Costruttore
    public Entity()
    {
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
}
