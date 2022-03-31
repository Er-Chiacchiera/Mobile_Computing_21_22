using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private float score = 0.0f;
    public TextMeshProUGUI scoreOutput;

    private Spawn spawner;
    [SerializeField]
    private float spawnRate = 0.15f;

    public GameObject wrench;
    public GameObject policeCar;
    public GameObject helicopter;
    public GameObject militaryJeep;
    public GameObject playerBody;

    //GameOver Stuff
    private bool gameHasEnded = false;
    public GameObject gameOverMenu; 

    void Start()
    {
        spawner = this.GetComponent<Spawn>();
        StartCoroutine(spawner.dropInsideMap(wrench, 10, 99, playerBody, 10));
        StartCoroutine(spawner.dropInsideMap(policeCar, 4, spawnRate, playerBody, 1));
        StartCoroutine(spawner.RandomDrop(helicopter, 2, spawnRate, playerBody, 3));
        StartCoroutine(spawner.dropInsideMap(militaryJeep, 3, (spawnRate-0.05f), playerBody, 4));
    }

    void Update()
    {
        scoreOutput.text = score.ToString();
    }

    public void setScore(int value){ this.score = value; }
    public float getScore(){ return this.score; }
    public void updateScore(float value) { this.score += value; }

    public void gameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Time.timeScale = 0f;
            gameOverMenu.GetComponentInChildren<TextMeshProUGUI>().text = "Current Score: " +  score.ToString();
            gameOverMenu.SetActive(true);
        }
    }

}
