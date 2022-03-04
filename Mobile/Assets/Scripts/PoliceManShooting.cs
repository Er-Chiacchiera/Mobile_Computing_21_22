using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManShooting : MonoBehaviour
{
    //variabili poliziotto
    private Vector2 direction;
    public Rigidbody2D policeMan; 
    private float angle;
    public Transform firePoint;
    private float fireRate;
    

    public GameObject bulletPrefab;
    public float bulletVelocity = 5;

    private GameObject robot;
    private Transform robotPosition;

    //variabile che serve per la meccanica dello sparo
    private float lastShot;

    private void Start()
    {
        //variabile gameobject del robot
        robot = GameObject.Find("Robot");

        //posizione del robot (serve per la direzione in cui sparare)
        robotPosition = robot.GetComponent<Transform>();

        //prendo firerate del poliziotto
        fireRate = gameObject.GetComponent<PoliceMan>().GetFireRate();
        lastShot = Time.time;
    }
    private void Shoot()
    {
        lastShot = Time.time;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletVelocity, ForceMode2D.Impulse);
        //setting danno proiettile
        bullet.GetComponent<Bullet>().SetDmg(gameObject.GetComponent<PoliceMan>().GetDmg());
    }

    /* update:
     * calcola la direzione in cui guarda il poliziotto (senza ruotarlo)*/
    private void Update()
    {
        direction.x = robotPosition.position.x - policeMan.position.x;
        direction.y = robotPosition.position.y - policeMan.position.y;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
    }

    //fixed update che ruota il poliziotto verso il robot
    private void FixedUpdate()
    {
        policeMan.rotation = angle;

        if (Time.time > (1f / fireRate) + lastShot)
            Shoot();
    }
}


/*
*/