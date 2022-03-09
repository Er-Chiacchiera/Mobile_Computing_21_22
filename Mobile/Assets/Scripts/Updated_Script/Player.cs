using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Entity
{
    public Joystick movJoystick;
    public Joystick aimJoystick;

    private Vector2 movement;
    private Vector2 direction;

    static int id = 0;

    

    //shooting
    public Transform firePointDx;
    public Transform firePointSx;
    public GameObject bulletPrefab;

    private float lastShot = 0.0f;
    //distanza joystick per sparare
    private float distanzaJ = 0.5f;

    //velocità proiettile
    [SerializeField]
    public float bulletVelocity = 5;


    public Player() : base(10, 7, 100, 1.5f, id)
    {
        //altre cose qui
    }

    public Player(float dmg, float fireRate, float maxHealth, float speed) : base(dmg, fireRate, maxHealth, speed, id)
    {
        //altre cose qui
    }

    void Start()
    {

    }

    void Update()
    {

        //variabili
        float horizontalMove = 0f;
        float verticalMove = 0f;

        //valori spostamento orizzontale
        if (movJoystick.Horizontal > 0.2f)
            horizontalMove = this.getSpeed();
        else
            if (movJoystick.Horizontal < -0.2f)
            horizontalMove = -this.getSpeed();
        else
            horizontalMove = 0;

        //valori spostamento verticale
        if (movJoystick.Vertical > 0.2f)
            verticalMove = this.getSpeed();
        else
            if (movJoystick.Vertical < -0.2f)
            verticalMove = -this.getSpeed();
        else
            verticalMove = 0;

        //valore movimento
        movement = new Vector2(horizontalMove, verticalMove);

        //valore mira
        direction.x = aimJoystick.Horizontal;
        direction.y = aimJoystick.Vertical;

        //spostamento
        base.rigidBody.MovePosition(movement * Time.deltaTime);

    }


    private void FixedUpdate()
    {
        //movimento
        base.rigidBody.MovePosition(rigidBody.position + movement * base.getSpeed() * Time.fixedDeltaTime);

        //rotazione
        if (direction.x != 0 && direction.y != 0)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rigidBody.rotation = angle;
        }

        //shooting
        if ((direction.x < -distanzaJ || direction.x > distanzaJ || direction.y < -distanzaJ || direction.y > distanzaJ) && base.getFireRate() != 0 && (Time.time > (1f / base.getFireRate()) + lastShot))
        {
            Shoot(firePointDx);
            Shoot(firePointSx);
        }
    }


    //potrebbe essere conveniente spostarla in Entity
    public void RestoreHp(float value) //value espressa in percentuale
    {
        float newHealt = base.getHealth() + base.getMaxHealth() * value;

        if (base.getHealth() > base.getMaxHealth())  base.setHealth(newHealt); 
        else  base.setHealth(base.getMaxHealth()); 
        
        base.healthBar.SetHealth(this.getHealth());
    }

    public bool fullHealth()
    {
        return base.getHealth() == base.getMaxHealth();
    }

    void Shoot(Transform firePoint)
    {
        lastShot = Time.time;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletVelocity, ForceMode2D.Impulse);
        //setting danno proiettile destro
        bullet.GetComponent<Bullet>().SetDmg(base.getDmg()); //proprietaria della classe bullet
        bullet.GetComponent<Bullet>().SetId(base.getId());

    }


    public void OnDestroy()
    {
        //fai partire il game over!!
    }


}
