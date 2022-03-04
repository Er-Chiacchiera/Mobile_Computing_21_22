using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoliceMan : MonoBehaviour
{
    //dopo quanti secondi viene distrutto in automatico
    [SerializeField]
    private float time = 3;
    private float dmg = 0;
    void Start()
    {
        Destroy(gameObject, time);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet")
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
