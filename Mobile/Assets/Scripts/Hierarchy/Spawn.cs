using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawn : MonoBehaviour
{
    //spawn parameter
    [SerializeField]
    private float spawnRadius = 30;
    private int idProgression = 0;

    //mappa int-int (id, num) --> tiene traccia delle unità generate per categoria 
    private Dictionary<int, int> enemyTracer;
    //mappa int-int (id, num) --> tiene traccia delle unità generate da altre unità
    private Dictionary<int, int> generatedEnemyTracer;

    //player screen radius
    private float outsideScreenRadius = 2;

    //map dimension 
    private float xMin = -68.4f;
    private float xMax = 94.7f;
    private float yMin = -37.6f;
    private float yMax = 49.1f;

    //Island dimension 
    private float islandXMin = -48.3f;
    private float islandXMax =  74.6f;
    private float islandYMin = -25.5f;
    private float islandYMax = 36.3f;

    public GameObject footpath;
    public GameObject border;
    public Camera playerView;

    private void Start()
    {
        enemyTracer = new Dictionary<int, int>();
        generatedEnemyTracer = new Dictionary<int, int>();
    }

    public IEnumerator SpawnNearEnemy(Animator animator, GameObject Spawner, GameObject spawnSubject, int maxSpawn, float spawnRate, int id)
    {
        float waitingSecond = 1 / spawnRate;
        int spawnCounter = 0;

        if (generatedEnemyTracer.ContainsKey(id))
            spawnCounter = generatedEnemyTracer[id];
        else
            generatedEnemyTracer.Add(id, 0);

        if (spawnCounter < maxSpawn)
        {
            
            
            Vector3 spawnPos = Spawner.GetComponent<Transform>().position;
            float rot = Spawner.GetComponent<Rigidbody2D>().rotation;

            float dis = 1;
            spawnPos.x = spawnPos.x + dis * Mathf.Cos(rot * Mathf.Deg2Rad);
            spawnPos.y = spawnPos.y + dis * Mathf.Sin(rot * Mathf.Deg2Rad);

            if (!footpath.GetComponent<Tilemap>().HasTile(new Vector3Int((int)spawnPos.x, (int)spawnPos.y)) && !border.GetComponent<Tilemap>().HasTile(new Vector3Int((int)spawnPos.x, (int)spawnPos.y)))
            {
                animator.SetTrigger("Open");
                generatedEnemyTracer[id] += 1;
                Instantiate(spawnSubject, spawnPos, new Quaternion(0, 0, rot, 0)).GetComponent<Enemy>().setGenerationId(id);
            }
            else waitingSecond = 0;
           
           
        }
        yield return new WaitForSeconds(waitingSecond);
        yield return SpawnNearEnemy(animator, Spawner, spawnSubject, maxSpawn, spawnRate, id);
    }

    public IEnumerator SpawnNearPlayer(GameObject spawnSubject, int maxSpawn, float spawnRate, GameObject playerBody, int id)
    {
        float waitingSecond = 1 / spawnRate;
        int spawnCounter = 0;

        if (enemyTracer.ContainsKey(id))
            spawnCounter = enemyTracer[id];
        else
            enemyTracer.Add(id, 0);

        if (spawnCounter < maxSpawn)
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

            if (!footpath.GetComponent<Tilemap>().HasTile(new Vector3Int((int)spawnPos.x, (int)spawnPos.y)) && !border.GetComponent<Tilemap>().HasTile(new Vector3Int((int)spawnPos.x, (int)spawnPos.y)))
            {
                enemyTracer[id] += 1;
                //spawn
                Instantiate(spawnSubject, spawnPos, Quaternion.identity).GetComponent<Enemy>().setProgId(idProgression += 1);
            }
            else waitingSecond = 0;


        }
        

        yield return new WaitForSeconds(waitingSecond);
        yield return SpawnNearPlayer(spawnSubject, maxSpawn, spawnRate, playerBody, id);
    }

    public IEnumerator RandomDrop(GameObject spawnSubject, int maxSpawn, float spawnRate, int id)
    {
        float waitingSecond = 1 / spawnRate;
        int spawnCounter = 0;

        if (enemyTracer.ContainsKey(id))
            spawnCounter = enemyTracer[id];
        else
            enemyTracer.Add(id, 0);

        if (spawnCounter < maxSpawn)
        {
            float xPos = Random.Range(xMin, xMax);
            float yPos = Random.Range(yMin, yMax);

            Vector2 spawnPos = new Vector2(xPos, yPos);


            if (!playerView.rect.Contains(spawnPos))
            {
                Instantiate(spawnSubject, spawnPos, Quaternion.identity).GetComponent<Enemy>().setProgId(idProgression += 1);
                enemyTracer[id] += 1;
            }
            else waitingSecond = 0;
            
        }

        yield return new WaitForSeconds(waitingSecond);
        yield return RandomDrop(spawnSubject, maxSpawn, spawnRate, id);
    }


    public IEnumerator dropInsideMap(GameObject spawnSubject, int maxSpawn, float spawnRate, int id)
    {
        float waitingSecond = 1 / spawnRate;
        int spawnCounter = 0;

        if (enemyTracer.ContainsKey(id))
            spawnCounter = enemyTracer[id];
        else
            enemyTracer.Add(id, 0);

        if (spawnCounter < maxSpawn)
        {

            float xPos = Random.Range(islandXMin, islandXMax);
            float yPos = Random.Range(islandYMin, islandYMax);
            Vector2 spawnPos = new Vector2(xPos, yPos);

            if (!footpath.GetComponent<Tilemap>().HasTile(new Vector3Int((int)xPos, (int)yPos)) && !border.GetComponent<Tilemap>().HasTile(new Vector3Int((int)xPos, (int)yPos)) && !playerView.rect.Contains(spawnPos))
            {
                enemyTracer[id] += 1;

                GameObject obj = Instantiate(spawnSubject, spawnPos, Quaternion.identity);
                if (obj.GetComponent<Enemy>() != null) obj.GetComponent<Enemy>().setProgId(idProgression += 1);
            }
            else waitingSecond = 0;
                        
        }

        yield return new WaitForSeconds(waitingSecond);
        yield return dropInsideMap(spawnSubject, maxSpawn, spawnRate, id);
    }

    public void UpdetEnemyTracer(int key) { if (enemyTracer.ContainsKey(key)) enemyTracer[key] -= 1; }

    public void UpdetGeneratedEnemyTracer(int key) { if (generatedEnemyTracer.ContainsKey(key)) generatedEnemyTracer[key] -= 1; }

    public void RemoveGenerationUnit(int key) { if (generatedEnemyTracer.ContainsKey(key)) generatedEnemyTracer.Remove(key); }



}
