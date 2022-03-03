using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMan : MonoBehaviour
{
    [SerializeField]
    private float dmg = 10;
    private float fireRate = 7;
    private float healthMax = 100;
    private float health;
    void Start()
    {
        health = healthMax;
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
