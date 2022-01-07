using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Robot")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Robot>().RestoreHp20();
        }
    }
}
