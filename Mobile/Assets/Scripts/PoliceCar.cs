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

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //se la collisione è con un proiettile
        if (collision.gameObject.tag == "Bullet")
        {
            //danni proiettile
            float dmg = collision.gameObject.GetComponent<Bullet>().GetDmg();
            health -= dmg;
        }
    }
}
