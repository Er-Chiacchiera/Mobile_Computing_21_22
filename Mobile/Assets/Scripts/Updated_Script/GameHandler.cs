using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameHandler : MonoBehaviour
{
    private float score = 0;
    private bool isLost = false;

    private Entity player;
    private List<Entity> enemies;

    [SerializeField]
    private float spawnRadius = 7;
    [SerializeField]
    private float carSpawnRate = 0.3f;
    [SerializeField]
    private float helicopterSpawnRate = 0.3f;

    public GameObject car;
    public GameObject helicopter;
    public GameObject robot;
    private float outsideScreenRadius = 2;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(car, carSpawnRate));
        //StartCoroutine(Spawn(helicopter, helicopterSpawnRate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScore(int value){ this.score = value; }
    public float getScore(){ return this.score; }

    public void setMatchState(bool value) { this.isLost = value; }
    public bool getMatchState() { return this.isLost; }

    public void setPlayer(Entity value) { this.player = value; }
    public Entity getPlayer() { return this.player; }

    IEnumerator Spawn(GameObject enemy, float spawnRate)
    {
        //posizione robot
        Vector2 spawnPos = robot.transform.position;

        //direzione spawn rispetto al robot
        Vector2 direction = Random.insideUnitCircle.normalized;

        //spostando lo spawn fuori dallo schermo
        float angle = Mathf.Atan2(direction.y, direction.x);
        direction.x = outsideScreenRadius * Mathf.Cos(angle);
        direction.y = outsideScreenRadius * Mathf.Sin(angle);
        spawnPos += direction * spawnRadius;

        //settando target della auto
        // car.GetComponent<DestinationAICustom>().target = robot.GetComponent<Transform>();

        //spawn
        Instantiate(enemy, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(1 / spawnRate);
        StartCoroutine(Spawn(enemy, spawnRate));
    }

}
