using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAiming : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Joystick joystick;
    private Vector2 direzione;

    void Update()
    {
        direzione.x = joystick.Horizontal;
        direzione.y = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        if (direzione.x != 0 && direzione.y != 0)
        {
            float angle = Mathf.Atan2(direzione.y, direzione.x) * Mathf.Rad2Deg - 90f;
            rigidBody.rotation = angle;
        }
    }
}
