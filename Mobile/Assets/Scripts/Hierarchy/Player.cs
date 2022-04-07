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
    public float totalLerpTime = 2f;
    public Image frontHealthBar;
    public Image redBackHealthBar;
    public Image greenBackHealthBar;
    public TextMeshProUGUI healthBarText;


    //shield
    private bool shieldActivated;
    private float shieldValue;
    [SerializeField]
    private float maxShieldValue = 50f;
    public TextMeshProUGUI shielsBarText;

    //shield bar stuff
    public Image frontShieldBar;

    public Player() : base() { }

    new void Start()
    {
        base.Start();
        base.setId(id);

        shieldValue = maxShieldValue;
        shieldActivated = true;
    }

    void Update()
    {
        UpdateHealthUI();
        UpdateShieldUI();
        HealthPlusMinus();
        setHealth(Mathf.Clamp(getHealth(), 0, getMaxHealth()));
        healthBarText.text = base.getHealth().ToString() + "/" + base.getMaxHealth().ToString();
        shielsBarText.text = ((int)shieldValue).ToString() + "/" + ((int)maxShieldValue).ToString();

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

        RechargeShield(0.15f);
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
        lerpTimerHealthBar = 0f;
        if (newHealt < base.getMaxHealth()) base.setHealth(newHealt);
        else base.setHealth(base.getMaxHealth());

    }

    public bool FullHealth()
    {
        return base.getHealth() == base.getMaxHealth();
    }

    private void UpdateHealthUI()
    {
        float fillFrontBar = frontHealthBar.fillAmount;
        float fillRedBar = redBackHealthBar.fillAmount;
        float healthFraction = getHealth() / getMaxHealth();

        if (fillRedBar > healthFraction) //se true significa che il player ha preso danno
        {
            frontHealthBar.fillAmount = healthFraction;
            greenBackHealthBar.fillAmount = healthFraction;

            float percentComplete = lerpTimerHealthBar / totalLerpTime;
            if (lerpTimerHealthBar < totalLerpTime)
            {
                lerpTimerHealthBar += Time.deltaTime;
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

            float percentComplete = lerpTimerHealthBar / totalLerpTime;
            if (lerpTimerHealthBar + 0.1f < totalLerpTime)
            {
                lerpTimerHealthBar += Time.deltaTime;
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
            lerpTimerHealthBar = 0f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            subHealth(-10f);
            lerpTimerHealthBar = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //se coolide con un proiettile 
        if (collision.gameObject.tag == "Bullet" && base.getId() != collision.gameObject.GetComponent<Bullet>().GetId())
        {
            //danni proiettile
            float currDmg = collision.gameObject.GetComponent<Bullet>().GetDmg();
            if (shieldActivated)
            {
                shieldValue -= currDmg;
                if (shieldValue <= 0)
                {
                    shieldActivated = false;
                    shieldValue = 0;
                }
            }
            else
            {
                base.subHealth(currDmg);
                lerpTimerHealthBar = 0f;
            }
        }
    }

    private void UpdateShieldUI()
    {
        frontShieldBar.fillAmount = shieldValue / maxShieldValue;
    }

    private void RechargeShield(float valuePerFrame)
    {
        if (shieldActivated)
        {
            if (shieldValue < maxShieldValue)
            {
                shieldValue += valuePerFrame;
            }
        }
    }

}
