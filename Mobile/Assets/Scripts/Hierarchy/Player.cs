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

    public Player() : base() { }

    new void Start()
    {
        base.Start();
        base.setId(id);
    }

    void Update()
    {
        UpdateHealthUI();
        HealthPlusMinus();
        setHealth(Mathf.Clamp(getHealth(), 0, getMaxHealth()));
        healthBarText.text = base.getHealth().ToString() + "/" + base.getMaxHealth().ToString();

        if (base.getHealth() <= 0)
        {
            FindObjectOfType<GameHandler>().gameOver();
        }

        //valore movimento
        movement = CalcoloSpostamento();
        //spostamento
        Sposta();

    }


    private void FixedUpdate()
    {
        //movimento
        base.rigidBody.MovePosition(rigidBody.position + movement * base.getSpeed() * Time.fixedDeltaTime);
    }

    private Vector2 CalcoloSpostamento()
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
        return new Vector2(horizontalMove, verticalMove);
    }

    private void Sposta()
    {
        base.rigidBody.MovePosition(movement * Time.deltaTime);
    }

    public void RestoreHp(float value) //value espressa in percentuale
    {
        float newHealt = base.getHealth() + base.getMaxHealth() * value;

        if (newHealt < base.getMaxHealth()) base.setHealth(newHealt);
        else base.setHealth(base.getMaxHealth());
    }

    public bool fullHealth()
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
    public void OnDestroy()
    {
        //fai partire il game over!!
    }

}
