using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private int idWeapon = 1;

    //shooting
    public Transform firePointDx;
    public Transform firePointSx;
    public Joystick aimJoystick;

    public GameObject bulletPrefabBase;
    public GameObject bulletPrefabLaser;
    public GameObject bulletPrefabRazzo;

    private float lastShot = 0.0f;

    private readonly float distanzaJ = 0.5f; //distanza joystick dal centro per sparare

    //parametri proiettile base
    private readonly float baseFireRate = 9f;
    private readonly float baseDamage = 10f;
    private readonly float baseVelocity = 5f;
    //parametri proiettile laser

    //parametri proiettile razzo


    private Vector2 direction;

    private void Update()
    {
        //valore mira
        direction.x = aimJoystick.Horizontal;
        direction.y = aimJoystick.Vertical;
    }

    private void FixedUpdate()
    {
        //rotazione
        if (direction.x != 0 && direction.y != 0)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            gameObject.GetComponent<Player>().rigidBody.rotation = angle;
        }

        if (idWeapon == 1) Shoot(bulletPrefabBase, baseFireRate, baseDamage, baseVelocity);
        if (idWeapon == 2) Shoot(bulletPrefabLaser, 30f, 3f, 40.0f);
        if (idWeapon == 3) Shoot(bulletPrefabRazzo, 1, 10, 5.0f);

    }

    public void Shoot(GameObject bulletPrefab, float fireRate, float dmg, float velocity)
    {
        if ((direction.x < -distanzaJ || direction.x > distanzaJ || direction.y < -distanzaJ || direction.y > distanzaJ) && fireRate != 0 && (Time.time > (1f / fireRate) + lastShot))
        {
            lastShot = Time.time;
            int idRobot = gameObject.GetComponent<Player>().getId();

            //sx
            GameObject bullet1 = Instantiate(bulletPrefab, firePointSx.position, firePointSx.rotation);
            Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
            rb1.AddForce(firePointSx.up * velocity, ForceMode2D.Impulse);
            bullet1.GetComponent<Bullet>().SetDmg(dmg);
            bullet1.GetComponent<Bullet>().SetId(idRobot);

            //dx
            GameObject bullet2 = Instantiate(bulletPrefab, firePointDx.position, firePointDx.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(firePointDx.up * velocity, ForceMode2D.Impulse);
            bullet2.GetComponent<Bullet>().SetDmg(dmg);
            bullet2.GetComponent<Bullet>().SetId(idRobot);
        }
    }

    public void SetIdWeapon(int idWeapon)
    {
        this.idWeapon = idWeapon;
    }
}
