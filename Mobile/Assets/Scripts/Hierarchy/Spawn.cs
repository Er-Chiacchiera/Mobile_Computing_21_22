using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //spawn parameter
    [SerializeField]
    private float spawnRadius = 7;
    private int idProgression = 0;

    //mappa int-int (id, num) --> tiene traccia delle unità generate per categoria 
    private Dictionary<int, int> enemyTracer;
    //mappa int-int (id, num) --> tiene traccia delle unità generate da altre unità
    private Dictionary<int, int> generatedEnemyTracer;

    //player screen radius
    private float outsideScreenRadius = 2;

    //map dimension 
    private float xMin = -28;
    private float xMax = 93;
    private float yMin = -35;
    private float yMax = 70;

    private void Start()
    {
        enemyTracer = new Dictionary<int, int>();
        generatedEnemyTracer = new Dictionary<int, int>();
    }

    public IEnumerator SpawnNearEnemy(Animator animator, GameObject Spawner, GameObject spawnSubject, int maxSpawn, float spawnRate, int id)
    {
        int spawnCounter = 0;

        if (generatedEnemyTracer.ContainsKey(id))
            spawnCounter = generatedEnemyTracer[id];
        else
            generatedEnemyTracer.Add(id, 0);    

        if (spawnCounter < maxSpawn)
        {
            animator.SetTrigger("Open");
            generatedEnemyTracer[id] += 1;
            Vector3 spawnPos = Spawner.GetComponent<Transform>().position;
            float rot = Spawner.GetComponent<Rigidbody2D>().rotation;

            float dis = 1;
            spawnPos.x = spawnPos.x + dis * Mathf.Cos(rot * Mathf.Deg2Rad);
            spawnPos.y = spawnPos.y + dis * Mathf.Sin(rot * Mathf.Deg2Rad);
            Instantiate(spawnSubject, spawnPos, new Quaternion(0, 0, rot, 0)).GetComponent<Enemy>().setGenerationId(id);
        }
        yield return new WaitForSeconds(1 / spawnRate);
        yield return SpawnNearEnemy(animator, Spawner, spawnSubject, maxSpawn, spawnRate, id);
    }

    public IEnumerator SpawnNearPlayer(GameObject spawnSubject, int maxSpawn, float spawnRate, GameObject playerBody, int id)
    {
        int spawnCounter = 0;

        if (enemyTracer.ContainsKey(id))
            spawnCounter = enemyTracer[id];
        else
            enemyTracer.Add(id, 0);

        if (spawnCounter < maxSpawn)
        {
            enemyTracer[id] += 1;
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
            Instantiate(spawnSubject, spawnPos, Quaternion.identity).GetComponent<Enemy>().setProgId(idProgression+=1);
            
        }
        yield return new WaitForSeconds(1 / spawnRate);
        yield return SpawnNearPlayer(spawnSubject, maxSpawn, spawnRate, playerBody, id);
    }

    public IEnumerator enemyDrop(GameObject spawnSubject, int maxSpawn, float spawnRate, GameObject playerBody, int id)
    {
        int spawnCounter = 0;

        if (enemyTracer.ContainsKey(id))
            spawnCounter = enemyTracer[id];
        else
            enemyTracer.Add(id, 0);

        if (spawnCounter < maxSpawn)
        {
            float xPos = Random.Range(xMin, xMax);
            float yPos = Random.Range(yMin, yMax);

            Instantiate(spawnSubject, new Vector2(xPos, yPos), Quaternion.identity).GetComponent<Enemy>().setProgId(idProgression += 1);
            enemyTracer[id] += 1;
        }

        yield return new WaitForSeconds(1 / spawnRate);
        yield return enemyDrop(spawnSubject, maxSpawn, spawnRate, playerBody, id);
    }

    public void UpdetEnemyTracer (int key) { if(enemyTracer.ContainsKey(key)) enemyTracer[key] -= 1; }

    public void UpdetGeneratedEnemyTracer(int key) { if (generatedEnemyTracer.ContainsKey(key)) generatedEnemyTracer[key] -= 1; }

    public void RemoveGenerationUnit(int key) { if (generatedEnemyTracer.ContainsKey(key)) generatedEnemyTracer.Remove(key); }



}
