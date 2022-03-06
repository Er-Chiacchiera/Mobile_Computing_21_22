using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 0.6f;
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Robot")
        {
            GameObject robot = collision.gameObject;

            if (!robot.GetComponent<Robot>().fullHealth())
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<Robot>().RestoreHp20();
            }
        }
    }
}
