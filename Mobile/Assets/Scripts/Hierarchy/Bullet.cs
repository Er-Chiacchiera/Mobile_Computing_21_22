using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //dopo quanti secondi viene distrutto in automatico
    [SerializeField]
    private float time = 3;
    private float dmg = 30;
    public float bulletVelocity = 1f;
    private int id;
    private Rigidbody2D rb;

    //homing stuff
    [SerializeField]
    private bool isHoming = false;
    private Transform target;


    //
    private float lerpTimerRocket;
    private readonly float totalLerpTimerRocket = 3f;
    public Bullet(float time, float dmg, GameObject bullet, int id)
    {
        this.time = time;
        this.dmg = dmg;
        this.id = id;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!isHoming) rb.AddForce(transform.up * bulletVelocity, ForceMode2D.Impulse);
        lerpTimerRocket = 0f;
        Destroy(gameObject, time);

        if (isHoming)
        {
            target = FindTarget();
        }
    }

    void FixedUpdate()
    {
        if (isHoming)
        {
            if (target == null)
            {
                target = FindTarget();
            }
            //HomeStorica();
            Vector2 point2target = (Vector2)transform.position - (Vector2)target.position;
            point2target.Normalize();
            float value = Vector3.Cross(point2target, transform.up).z;
            rb.angularVelocity = 200f * value;
            rb.velocity = transform.up * bulletVelocity;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (id != 0)
        {
            if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet" )
                Destroy(gameObject);
        }
        else //id = 0
        {
            if (collision.gameObject.tag != "Robot" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Ability") 
                Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ability" && id != 0) dmg = 0;
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

    private Transform FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = Mathf.Infinity;
        float dist;
        Transform target = null;
        foreach (GameObject currTarget in targets)
        {
            dist = Vector3.Distance(transform.position, currTarget.GetComponent<Transform>().position);
            if (dist < minDist)
            {
                target = currTarget.GetComponent<Transform>();
                minDist = dist;
            }
        }

        return target;
    }
    private void HomeStorica()
    {
        if (target == null)
        {
            target = FindTarget();
        }

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        lerpTimerRocket += Time.deltaTime;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float angleOriginal = gameObject.GetComponent<Rigidbody2D>().rotation;
        float percent = lerpTimerRocket / 10f;
        if (lerpTimerRocket > 3f) percent = 1;
        if (angle != angleOriginal)
            gameObject.GetComponent<Rigidbody2D>().rotation = (1 - percent) * angleOriginal + (angle * percent);

        rb.velocity = transform.up * bulletVelocity;
    }
}