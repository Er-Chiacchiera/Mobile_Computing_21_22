using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    private float score = 0.0f;
    public TextMeshProUGUI scoreOutput;

    private Spawner spawner;
    [SerializeField]
    private float spawnRate = 0.3f;
    public GameObject car;
    public GameObject playerBody;

    //private Entity player;
    //private List<Entity> enemies;

    void Start()
    {
        this.spawner = new Spawner(5, spawnRate, car, playerBody);
        StartCoroutine(spawner.SpawnNearPlayer());
        //StartCoroutine(enemyDrop(helicopter, helicopterSpawnRate));
    }

    void Update()
    {
        scoreOutput.text = score.ToString();
    }

    public void setScore(int value){ this.score = value; }
    public float getScore(){ return this.score; }
    public void updateScore(float value) { this.score += value; }

    //public void setPlayer(Entity value) { this.player = value; }
    //public Entity getPlayer() { return this.player; }


}
