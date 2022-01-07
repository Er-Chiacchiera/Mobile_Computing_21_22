using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float time = 3;
    private float dmg = 0;
    void Start()
    {
        Destroy(gameObject, time);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void SetDmg(float d)
    {
        dmg = d;
    }
    public float GetDmg()
    {
        return dmg;
    }
}