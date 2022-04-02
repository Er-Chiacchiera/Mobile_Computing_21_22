using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
public class Enemy : Entity
{

    //private static float onDeathTime = 10.0f;
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
    //despawn parameter
    private float maxOverTime = 8.0f;
    private float destroyTime = 0.0f;

    private IAstarAI ai;
    protected GameObject player;
    private float distance;

    //spawn-shooting parameter
    protected bool isInScope;
    protected Spawn spawn;
    private int generationId = -1; //identifica l'unità che mi ha generato (-1 valore non valido)
    protected int progId = -1; //identifica l'ordine di generazione del'unità
    [SerializeField] private float dmg;
    [SerializeField] private float fireRate;

    //healthbar
    public GameObject hpBar;
    private GameObject hpBarReference;
    private float verticalDistance = 1.7f;
    private float horizontalDistance = -0.5f;



    public Enemy() : base()
    {
    }


    public new void Start()
    {
        base.Start();
        SpawnHealthBar();
        spawn = GameObject.Find("GameController").GetComponent<Spawn>();
        game = GameObject.Find("GameController").GetComponent<GameHandler>();
        ai = GetComponent<IAstarAI>();
        ai.maxSpeed = base.getSpeed();
        ai.onSearchPath += Update;
    }

    public void Update()
    {
        UpdateHealthBar();

        if (player.GetComponent<Transform>().position != null && ai != null) ai.destination = player.GetComponent<Transform>().position;
        distance = ai.remainingDistance;

        if (distance <= stopDistance)
        {
            isInScope = true;
            this.ai.isStopped = true;
            //this.enabled = false;
        }

        if (distance > stopDistance)
        {
            this.ai.isStopped = false;
            this.isInScope = false;
        }

        if(getHealth() <= 0)
        {
            Destroy(gameObject);
            game.updateScore(grantScore);

            //da valutare le seguenti aggiunte
            /*
            Destroy(gameObject, onDeathTime);
            animator.SetTrigger("Death");
            this.ai.isStopped = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.enabled = false;*/
        }


    }

    private void FixedUpdate()
    {
        if (distance > limitDistance && destroyTime == 0)
        {
            destroyTime = Time.time + maxOverTime;
        }

        if (Time.time > destroyTime)
        {
            if (distance > limitDistance)
            {
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

        Destroy(hpBarReference);
        //aggiorno le statistiche dello spawner
        if (generationId != -1)
            spawn.UpdetGeneratedEnemyTracer(generationId);
        else
        {
            spawn.UpdetEnemyTracer(getId());
            spawn.RemoveGenerationUnit(progId);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //se coolide con un proiettile 
        if (collision.gameObject.tag == "Bullet" && base.getId() != collision.gameObject.GetComponent<Bullet>().GetId())
        {
            //danni proiettile
            float currDmg = collision.gameObject.GetComponent<Bullet>().GetDmg();
            base.subHealth(currDmg);
            lerpTimerHealthBar = 0f;
        }
    }

    public void setGenerationId (int value) {this.generationId = value; }

    public void setProgId(int value) { this.progId = value; }

    public void setDmg(float value) { this.dmg = value; }
    public float getDmg() { return this.dmg; }

    public void setFireRate(float value) { this.fireRate = value; }
    public float getFireRate() { return this.fireRate; }

}

