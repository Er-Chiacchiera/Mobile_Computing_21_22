using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Player : Entity
{
    static int id = 0;

    //Joystick
    public Joystick movJoystick;
    private Vector2 movement;

    //health bar stuff (float lerpTimer in Entity.cs)
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image redBackHealthBar;
    public Image greenBackHealthBar;
    public TextMeshProUGUI healthBarText;

    public Image frontShieldBar;

    public Player() : base() { }

    new void Start()
    {
        base.Start();
        base.setId(id);
    }

    void Update()
    {
        UpdateHealthUI();
        //HealthPlusMinus();
        setHealth(Mathf.Clamp(getHealth(), 0, getMaxHealth()));
        healthBarText.text = base.getHealth().ToString() + "/" + base.getMaxHealth().ToString();

        if (base.getHealth() <= 0)
        {
            FindObjectOfType<GameHandler>().gameOver();
        }

        CalcoloSpostamento();
    }


    private void FixedUpdate()
    {
        //movimento
        base.rigidBody.MovePosition(rigidBody.position + movement * base.getSpeed() * Time.fixedDeltaTime);
    }

    private void CalcoloSpostamento()
    {
        float horizontalMove = 0f;
        float verticalMove = 0f;

        //valori spostamento orizzontale
        if (movJoystick.Horizontal > 0.2f)
            horizontalMove = this.getSpeed();
        else
            if (movJoystick.Horizontal < -0.2f)
            horizontalMove = -this.getSpeed();

        //valori spostamento verticale
        if (movJoystick.Vertical > 0.2f)
            verticalMove = this.getSpeed();
        else
            if (movJoystick.Vertical < -0.2f)
            verticalMove = -this.getSpeed();

        //valore movimento
        movement.x = horizontalMove;
        movement.y = verticalMove;
    }

    public void RestoreHp(float value) //value espressa in percentuale
    {
        float newHealt = base.getHealth() + base.getMaxHealth() * value;

        if (newHealt < base.getMaxHealth()) base.setHealth(newHealt);
        else base.setHealth(base.getMaxHealth());
    }

    public bool FullHealth()
    {
        return base.getHealth() == base.getMaxHealth();
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

        if (fillFrontBar < healthFraction) //se true significa che il player si è curato
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

    private void HealthPlusMinus()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //se coolide con un proiettile 
        if (collision.gameObject.tag == "Bullet" && base.getId() != collision.gameObject.GetComponent<Bullet>().GetId())
        {
            //danni proiettile
            float currDmg = collision.gameObject.GetComponent<Bullet>().GetDmg();
            base.subHealth(currDmg);
            lerpTimer = 0f;
        }
    }

    public void OnDestroy()
    {
        //fai partire il game over!!
    }

}
