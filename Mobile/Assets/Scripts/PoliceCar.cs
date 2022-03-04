using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    [SerializeField]
    private float health;
    void Start()
    {
        health = 100;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //se la collisione � con un proiettile
        if (collision.gameObject.tag == "Bullet")
        {
            //danni proiettile
            float dmg = collision.gameObject.GetComponent<Bullet>().GetDmg();
            health -= dmg;
            if (health <= 0) Destroy(gameObject);
        }
    }
}
