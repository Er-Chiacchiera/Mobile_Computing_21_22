using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPoliceCar : MonoBehaviour
{
    [SerializeField]
    private float spawnRadius = 7;
    [SerializeField]
    private float spawnRate = 0.3f;
    public GameObject car;
    public GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPoliceCar());
    }

    IEnumerator SpawnPoliceCar()
    {
        Vector2 spawnPos = robot.transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(car, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(1/spawnRate);
        StartCoroutine(SpawnPoliceCar());
    }
}
