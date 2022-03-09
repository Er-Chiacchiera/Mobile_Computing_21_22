using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //dopo quanti secondi viene distrutto in automatico
    [SerializeField]
    private float time = 3;
    private float dmg = 30;
    public float bulletVelocity;
    private int id;

    public Bullet(float time, float dmg, GameObject bullet, int id)
    {
        this.time = time;
        this.dmg = dmg;
        this.id = id;
    }

    void Start()
    {
        Destroy(gameObject, time);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Robot" && collision.gameObject.tag != "Bullet" )
            Destroy(gameObject);
    }

    public void SetDmg(float value)
    {
        dmg = value;
    }
    public float GetDmg()
    {
        return dmg;
    }

    public void SetId(int value)
    {
        id = value;
    }
    public float GetId()
    {
        return id;
    }
}