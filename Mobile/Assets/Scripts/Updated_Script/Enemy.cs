using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
public class Enemy : Entity 
{
public Animator animator;

    //pathfinder parameter
    [SerializeField]
    protected float stopDistance = 10;

    //spawn parameter
    [SerializeField]
    private bool isSpawner;
    [SerializeField]
    private int maxSpawn = 0;
    [SerializeField]
    private float spawnRate = 5.0f;


    /// <summary>The object that the AI should move to</summary>
    IAstarAI ai;
    private GameObject player;
    public GameObject spawnSubject;
    
    private int currSpawn = 0;
    private float nextSpawnTime = 0.0f;
    private float distance;
    protected bool isInScope ;
        

    public Enemy(float dmg, float fireRate, float maxHealth, float speed) : base(dmg, fireRate, maxHealth, speed)
    {
    }


    public void Start()
    {
        ai = GetComponent<IAstarAI>();
        ai.onSearchPath += Update;
    }

    public void Update()
    {
        

        if (player.GetComponent<Transform>().position != null && ai != null) ai.destination = player.GetComponent<Transform>().position;
        distance = ai.remainingDistance;

        
        if (distance <= stopDistance)
        {

            if (isSpawner && currSpawn < maxSpawn && Time.time > nextSpawnTime)
            {
                //aggiorno il timer
                nextSpawnTime = Time.time + spawnRate;

                animator.SetTrigger("Open");
                currSpawn += 1;

                Vector3 spawnPos = gameObject.GetComponent<Transform>().position;
                float rot = gameObject.GetComponent<Rigidbody2D>().rotation;

                float dis = 1;
                spawnPos.x = spawnPos.x + dis * Mathf.Cos(rot * Mathf.Deg2Rad);
                spawnPos.y = spawnPos.y + dis * Mathf.Sin(rot * Mathf.Deg2Rad);
                Instantiate(spawnSubject, spawnPos, new Quaternion(0, 0, rot, 0));
            }

            isInScope = true;
            this.ai.isStopped = true;
            //this.enabled = false;
        }

        if (distance > stopDistance)
        {
            this.ai.isStopped = false;
            this.isInScope = false;
        }

    }

    private void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
        if (ai != null) ai.onSearchPath += Update;
        player = GameObject.Find("Robot");
    }

    void OnDisable()
    {
        if (ai != null) ai.onSearchPath -= Update;
    }

}

