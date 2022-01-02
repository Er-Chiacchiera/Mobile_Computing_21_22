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
    private RaycastHit2D hit;
    private BoxCollider2D boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0 )

        //valori spostamento x e x dal joystick
        horizontalMove = joystick.Horizontal * speed;
        verticalMove = joystick.Vertical * speed;
        movement = new Vector3(horizontalMove, verticalMove, 0);

        //spostamento
        transform.Translate(movement * Time.deltaTime);


    }
}
