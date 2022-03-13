using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameHandler : MonoBehaviour
{
    private float score = 0;

    //private Entity player;
    //private List<Entity> enemies;

    //spawn parameter
    [SerializeField]
    private float spawnRadius = 7;
    [SerializeField]
    private int maxSpawn = 0;
    [SerializeField]
    private float carSpawnRate = 0.3f;
    [SerializeField]
    private float helicopterSpawnRate = 0.3f;

    //valutare una mappa per i nemici (ocoorre definire i metodi equals)
    public GameObject car;
    public GameObject helicopter;
    public GameObject playerBody;
    private float outsideScreenRadius = 2;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Spawn(car, carSpawnRate));
        StartCoroutine(enemyDrop(helicopter, helicopterSpawnRate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScore(int value){ this.score = value; }
    public float getScore(){ return this.score; }

    //public void setPlayer(Entity value) { this.player = value; }
    //public Entity getPlayer() { return this.player; }

    public IEnumerator Spawn(GameObject enemy, float spawnRate)
    {
        //posizione robot
        Vector2 spawnPos = playerBody.transform.position;

        //direzione spawn rispetto al robot
        Vector2 direction = Random.insideUnitCircle.normalized;

        //spostando lo spawn fuori dallo schermo
        float angle = Mathf.Atan2(direction.y, direction.x);
        direction.x = outsideScreenRadius * Mathf.Cos(angle);
        direction.y = outsideScreenRadius * Mathf.Sin(angle);
        spawnPos += direction * spawnRadius;

        //spawn
        Instantiate(enemy, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(1 / spawnRate);
        StartCoroutine(Spawn(enemy, spawnRate));
    }

    public IEnumerator enemyDrop(GameObject enemy, float spawnRate)
    {
        int spawnCounter = 0;

        if (spawnCounter < maxSpawn)
        {
            float xPos = Random.Range(-28, 93);
            float yPos = Random.Range(-35, 70);

            Instantiate(enemy, new Vector2(xPos, yPos), Quaternion.identity);
            spawnCounter += 1;
            yield return new WaitForSeconds(1 / spawnRate);
            StartCoroutine(enemyDrop(enemy, spawnRate));
        }
    }
}
