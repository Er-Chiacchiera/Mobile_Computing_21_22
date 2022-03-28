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

    private Spawn spawner;
    [SerializeField]
    private float spawnRate = 0.15f;
    public GameObject policeCar;
    public GameObject helicopter;
    public GameObject militaryJeep;
    public GameObject playerBody;

    //private Entity player;
    //private List<Entity> enemies;

    void Start()
    {
        spawner = this.GetComponent<Spawn>();
        StartCoroutine(spawner.dropInsideMap(policeCar, 4, spawnRate, 1));
        StartCoroutine(spawner.RandomDrop(helicopter, 2, spawnRate, 3));
        StartCoroutine(spawner.dropInsideMap(militaryJeep, 3, (spawnRate-0.05f), 4));
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
