using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShooting : MonoBehaviour
{
    public Joystick joystick;
    public Transform firePointDx;
    public Transform firePointSx;
    public GameObject bulletPrefab;
    private Vector2 direzione;

    //distanza joystick per sparare
    private float distanzaJ = 0.5f;

    private float fireRate = 10f;
    private float lastShot = 0.0f;

    public float bulletForce = 5f;

    void Update()
    {
        direzione.x = joystick.Horizontal;
        direzione.y = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        if ((direzione.x < -distanzaJ || direzione.x > distanzaJ || direzione.y < -distanzaJ || direzione.y > distanzaJ) && (Time.time>(1f/fireRate)+lastShot))
            Shoot();
    }
    void Shoot()
    {
        lastShot = Time.time;
        //sparo da destra
        GameObject bulletDx = Instantiate(bulletPrefab, firePointDx.position, firePointDx.rotation);
        Rigidbody2D rbDx = bulletDx.GetComponent<Rigidbody2D>();
        rbDx.AddForce(firePointDx.up * bulletForce, ForceMode2D.Impulse);
        //sparo da sinistra
        GameObject bulletSx = Instantiate(bulletPrefab, firePointSx.position, firePointSx.rotation);
        Rigidbody2D rbSx = bulletSx.GetComponent<Rigidbody2D>();
        rbSx.AddForce(firePointSx.up * bulletForce, ForceMode2D.Impulse);
    }
}
