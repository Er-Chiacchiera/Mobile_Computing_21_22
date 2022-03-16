using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
public class Enemy : Entity
{
    public Animator animator;

    //score parameter
    [SerializeField]
    private float grantScore = 0.0f;
    private GameHandler game;

    //pathfinder parameter
    [SerializeField]
    protected float stopDistance = 10;
    [SerializeField]
    protected float limitDistance = 15;

    private float maxOverTime = 8.0f;
    private float destroyTime = 0.0f;

    IAstarAI ai;
    private GameObject player;
    public GameObject spawnSubject;
    private float distance;
    protected bool isInScope;

    //spawn parameter
    [SerializeField]
    private bool isSpawner;
    [SerializeField]
    private int maxSpawn = 0;
    [SerializeField]
    private float spawnRate = 5.0f;
    //private int currSpawn = 0;
    //private float nextSpawnTime = 0.0f;
    private Spawner spawner;

    //healthbar
    public GameObject hpBar;
    private GameObject hpBarReference;
    private float verticalDistance = 1.7f;
    private float horizontalDistance = -0.5f;



    public Enemy(float dmg, float fireRate, float maxHealth, float speed) : base(dmg, fireRate, maxHealth, speed)
    {
    }


    public void Start()
    {
        SpawnHealthBar();
        this.spawner = new Spawner(maxSpawn, spawnRate, spawnSubject, player);
        game = GameObject.Find("GameController").GetComponent<GameHandler>();
        ai = GetComponent<IAstarAI>();
        ai.onSearchPath += Update;
    }

    public void Update()
    {
        UpdateHealthBar();

        if (player.GetComponent<Transform>().position != null && ai != null) ai.destination = player.GetComponent<Transform>().position;
        distance = ai.remainingDistance;

        if (distance <= stopDistance)
        {

            if (isSpawner)
            {
                StartCoroutine(spawner.Spawn(this.gameObject, animator));
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

        if (distance > limitDistance && destroyTime == 0)
        {
            destroyTime = Time.time + maxOverTime;
        }

        if (Time.time > destroyTime)
        {
            if (distance > limitDistance)
            {
                this.grantScore = 0;
                Destroy(gameObject);
            }

            else
            {
                destroyTime = 0;
            }
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

    private void OnDestroy()
    {
        game.updateScore(grantScore);

        Destroy(hpBarReference);
        this.ai.isStopped = true;
    }

    private void SpawnHealthBar()
    {
        Vector2 spawnPos = gameObject.GetComponent<Transform>().position;
        spawnPos.x += horizontalDistance;
        spawnPos.y += verticalDistance;
        hpBarReference = Instantiate(hpBar, spawnPos, Quaternion.identity);
    }

    private void UpdateHealthBar()
    {
        Vector2 position = gameObject.GetComponent<Transform>().position;
        position.x += horizontalDistance;
        position.y += verticalDistance;

        Vector3 position3 = new Vector3(position.x, position.y, 0);
        hpBarReference.GetComponent<Transform>().position = position3;

        Vector3 scale = hpBarReference.GetComponentInChildren<Transform>().localScale;
        scale.x = getHealth() / getMaxHealth();
        hpBarReference.GetComponentInChildren<Transform>().localScale = scale;
    }



}

