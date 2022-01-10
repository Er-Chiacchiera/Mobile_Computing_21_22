using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private float dmg = 10;
    private float fireRate = 10;
    private float healthMax = 100;
    private float health;
    void Start()
    {
        health = healthMax;
    }


    // Update is called once per frame
    void Update()
    {
        
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
        health += healthMax * 0.2f;
        if (health > healthMax)
            health = healthMax;
    }
}
