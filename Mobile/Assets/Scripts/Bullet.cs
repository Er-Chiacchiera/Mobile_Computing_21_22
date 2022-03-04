using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //dopo quanti secondi viene distrutto in automatico
    [SerializeField]
    private float time = 3;
    private float dmg = 0;
    void Start()
    {
        Destroy(gameObject, time);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Robot" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "BulletEnemy")
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