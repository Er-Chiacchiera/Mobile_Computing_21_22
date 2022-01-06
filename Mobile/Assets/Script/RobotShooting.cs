using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShooting : MonoBehaviour
{
    public Joystick joystick;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Vector2 direzione;
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    }
}
