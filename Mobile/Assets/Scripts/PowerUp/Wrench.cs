using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : PowerUp
{

    [SerializeField] private float restoreValue = 0.3f;
    public override void interaction(Collider2D collision)
    {
        collision.gameObject.GetComponent<Player>().RestoreHp(restoreValue);
    }
}
