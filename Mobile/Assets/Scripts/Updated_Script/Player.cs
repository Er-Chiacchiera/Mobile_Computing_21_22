using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : Entity
{
    static int id = 0;

    //Joystick
    public Joystick movJoystick;
    public Joystick aimJoystick;
    private Vector2 movement;
    private Vector2 direction;

    //shooting
    public Transform firePointDx;
    public Transform firePointSx;
    public GameObject bulletPrefab;
    private float lastShot = 0.0f;
    private float distanzaJ = 0.5f; //distanza joystick per sparare
    [SerializeField]
    public float bulletVelocity = 5; //velocit� proiettile

    //health bar stuff (float lerpTimer in Entity.cs)
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image redBackHealthBar;
    public Image greenBackHealthBar;

    //forceField parameter
    private bool isEnable = false;
    public bool isActive = false;
    [SerializeField] private GameObject forceField;
    [SerializeField] private Button loadingButton;
    [SerializeField] private float countdown = 0;
    public float shutdown = 0;
    public float startingTime = 0;
    private LoadingCircle loadingCircle;




    public Player() : base(10, 7, 100, 1.5f) { }

    void Start()
    {
        base.setId(id);
        loadingCircle = this.GetComponent<LoadingCircle>();
    }

    void Update()
    {
        
        UpdateHealthUI();
        healthPlusMinus();
        setHealth(Mathf.Clamp(getHealth(), 0, getMaxHealth()));

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

        //forceField
        if (!isEnable)
        {
            if (Time.fixedTime - startingTime > countdown)
            {
                isEnable = true;
                loadingButton.interactable = true;

            }

            else
            {
                loadingCircle.progress = (Time.fixedTime - startingTime) / countdown;
            }

        }

        if (isActive)
        {
            loadingButton.interactable = false;
            loadingCircle.progress = 0;
            base.isImmune = true;

            if (Time.fixedTime - startingTime > shutdown)
            {
                forceField.SetActive(false);
                this.isImmune = false;
                isActive = false;
                isEnable = false;
                startingTime = Time.fixedTime;
                
            }
           
        }
        
    }


    public void RestoreHp(float value) //value espressa in percentuale
    {
        float newHealt = base.getHealth() + base.getMaxHealth() * value;

        if (newHealt < base.getMaxHealth())  base.setHealth(newHealt);
        else base.setHealth(base.getMaxHealth());
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
        bullet.GetComponent<Bullet>().SetDmg(base.getDmg()); 
        bullet.GetComponent<Bullet>().SetId(base.getId());
    }

    public void UpdateHealthUI()
    {
        float fillFrontBar = frontHealthBar.fillAmount;
        float fillRedBar = redBackHealthBar.fillAmount;
        float healthFraction = getHealth() / getMaxHealth();

        if (fillRedBar + 0.1f > healthFraction) //se true significa che il player ha preso danno
        {
            frontHealthBar.fillAmount = healthFraction;
            greenBackHealthBar.fillAmount = healthFraction;
            
            float percentComplete = lerpTimer / chipSpeed;
            if (lerpTimer < chipSpeed)
            {
                lerpTimer += Time.deltaTime;
                redBackHealthBar.fillAmount = Mathf.Lerp(fillRedBar, healthFraction, percentComplete);
            }
            else
            {
                redBackHealthBar.fillAmount = healthFraction;
            }            
        }

        if (fillFrontBar < healthFraction) //se true significa che il player si � curato
        {
            greenBackHealthBar.fillAmount = healthFraction;          

            float percentComplete = lerpTimer / chipSpeed;
            if (lerpTimer + 0.1f < chipSpeed)
            {
                lerpTimer += Time.deltaTime;
                frontHealthBar.fillAmount = Mathf.Lerp(fillFrontBar, healthFraction, percentComplete);
            }
            else
            {
                frontHealthBar.fillAmount = healthFraction;
                redBackHealthBar.fillAmount = healthFraction;
            }
        }

    }

    private void healthPlusMinus()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            subHealth(10f);
            lerpTimer = 0f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            subHealth(-10f);
            lerpTimer = 0f;
        }
    }
    public void OnDestroy()
    {
        //fai partire il game over!!
    }

}
