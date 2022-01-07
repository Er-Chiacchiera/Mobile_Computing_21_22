using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpawnerPoliceCar : MonoBehaviour
{
    [SerializeField]
    private float spawnRadius = 7;
    [SerializeField]
    private float spawnRate = 0.3f;
    public GameObject car;
    public GameObject robot;
    private float outsideScreenRadius = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPoliceCar());
    }

    IEnumerator SpawnPoliceCar()
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
        car.GetComponent<AIDestinationSetter>().target = robot.transform;

        //spawn
        Instantiate(car, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(1/spawnRate);
        StartCoroutine(SpawnPoliceCar());
    }
}
