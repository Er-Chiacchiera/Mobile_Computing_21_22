using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private float score = 0;
    private bool isLost = false;

    private Entity player;
    private List<Entity> enemies;

    
    // Start is called before the first frame update
    void Start()
    {
        player = new Player(10, 7, 100, 1.5f);
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

}
