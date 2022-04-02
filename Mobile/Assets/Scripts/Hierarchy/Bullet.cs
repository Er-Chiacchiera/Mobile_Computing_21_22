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
        Destroy(gameObject, time);
    }

    void FixedUpdate()
    {
        if (isHoming)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            float minDist = Mathf.Infinity;
            float dist;

            foreach(GameObject currTarget in targets)
            {
                dist = Vector3.Distance(transform.position, currTarget.GetComponent<Transform>().position);
                if(dist < minDist)
                {
                    target = currTarget.GetComponent<Transform>();
                    minDist = dist;
                }
            }

            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * 200f;
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
}