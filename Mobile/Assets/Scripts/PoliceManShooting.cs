using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManShooting : MonoBehaviour
{
    private Vector2 direzione;
    public Rigidbody2D rigidBody;
    private Transform posizioneRobot;
    private float angle;

    private void Start()
    {
        //variabile transform con la posizione del robot
        posizioneRobot = GameObject.Find("Robot").GetComponent<Transform>();
    }
    //update che calcola la direzione in cui guarda il poliziotto (senza ruotarlo)
    private void Update()
    {
        direzione.x = posizioneRobot.position.x - rigidBody.position.x;
        direzione.y = posizioneRobot.position.y - rigidBody.position.y;
        angle = Mathf.Atan2(direzione.y, direzione.x) * Mathf.Rad2Deg + 90f;
    }

    //fixed update che ruota il poliziotto verso il robot
    private void FixedUpdate()
    {
        rigidBody.rotation = angle;
    }
}

/*
 * 
 * 
public Transform firePoint;

public GameObject bulletPrefab;
private Vector2 direzione;
private float lastShot = 0.0f;

//firerate
[SerializeField]
private float fireRate;

//velocità proiettile
[SerializeField]
public float bulletVelocity = 5;

private void Start()
{
    fireRate = gameObject.GetComponent<Robot>().GetFireRate();
}

private void FixedUpdate()
{
    if (Time.time > (1f / fireRate) + lastShot)
        Shoot();
}
void Shoot()
{
    lastShot = Time.time;

    //sparo da destra
    GameObject bulletDx = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Rigidbody2D rbDx = bulletDx.GetComponent<Rigidbody2D>();
    rbDx.AddForce(firePoint.up * bulletVelocity, ForceMode2D.Impulse);
    //setting danno proiettile
    bulletDx.GetComponent<Bullet>().SetDmg(gameObject.GetComponent<Robot>().GetDmg());

}*/