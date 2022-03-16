using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //spawn parameter
    [SerializeField]
    private float spawnRadius = 7;
    [SerializeField]
    private int maxSpawn = 0;
    [SerializeField]
    private float spawnRate = 0.3f;

    public int spawnCounter = 0;

    //valutare una mappa per i nemici (ocoorre definire i metodi equals)
    public GameObject spawnSubject;
    public GameObject playerBody;

    //player screen radius
    private float outsideScreenRadius = 2;

    //map dimension 
    private float xMin = -28;
    private float xMax = 93;
    private float yMin = -35;
    private float yMax = 70;

    public Spawner(int maxSpawn, float spawnRate, GameObject spawnSubject, GameObject playerBody)
    {
        this.maxSpawn = maxSpawn;
        this.spawnRate = spawnRate;
        this.spawnSubject = spawnSubject;
        this.playerBody = playerBody;
    }

    public IEnumerator Spawn(GameObject spawner, Animator animator)
    {
        if (spawnCounter < maxSpawn)
        {
            animator.SetTrigger("Open");
            spawnCounter += 1;
            Vector3 spawnPos = spawner.GetComponent<Transform>().position;
            float rot = spawner.GetComponent<Rigidbody2D>().rotation;

            float dis = 1;
            spawnPos.x = spawnPos.x + dis * Mathf.Cos(rot * Mathf.Deg2Rad);
            spawnPos.y = spawnPos.y + dis * Mathf.Sin(rot * Mathf.Deg2Rad);
            Instantiate(spawnSubject, spawnPos, new Quaternion(0, 0, rot, 0));
            yield return new WaitForSeconds(1 / spawnRate);
            yield return Spawn(spawner,animator);
        }
    }

    public IEnumerator SpawnNearPlayer()
    {
        if(spawnCounter < maxSpawn) { 
        spawnCounter += 1;
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
        Instantiate(spawnSubject, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(1 / spawnRate);
        yield return SpawnNearPlayer();
        }
    }

    public IEnumerator enemyDrop()
    {
        if (spawnCounter < maxSpawn)
        {
        float xPos = Random.Range(xMin, xMax);
        float yPos = Random.Range(yMin, yMax);

        Instantiate(spawnSubject, new Vector2(xPos, yPos), Quaternion.identity);
        spawnCounter += 1;
        yield return new WaitForSeconds(1 / spawnRate);
        yield return enemyDrop();
        }
    }

}
