using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField]
    private float speed = 1.5f;
    public Rigidbody2D rigidBody;
    Vector2 movement;

    void Update()
    {
        //variabili
        float horizontalMove = 0f;
        float verticalMove = 0f;

        //valori spostamento orizzontale
        if (joystick.Horizontal > 0.2f)
            horizontalMove = speed;
        else
            if (joystick.Horizontal < -0.2f)
            horizontalMove = -speed;
        else
            horizontalMove = 0;

        //valori spostamento verticale
        if (joystick.Vertical > 0.2f)
            verticalMove = speed;
        else
            if (joystick.Vertical < -0.2f)
            verticalMove = -speed;
        else
            verticalMove = 0;
        
        //valore movimento
        movement = new Vector2(horizontalMove, verticalMove);

        //spostamento
        rigidBody.MovePosition(movement * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * speed * Time.fixedDeltaTime);
    }
}
