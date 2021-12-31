using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public Joystick joystick;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private float speed = 1f;
    private Vector3 movement;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = joystick.Horizontal * speed;
        verticalMove = joystick.Vertical * speed;

        movement = new Vector3(horizontalMove, verticalMove, 0);
        transform.Translate(movement * Time.deltaTime);
    }
}
