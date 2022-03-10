using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity //problema parzialmente risolto: ci sono nemici che non sparano
{
    public Animator animator;


    public Enemy(float dmg, float fireRate, float maxHealth, float speed, int id) : base(dmg, fireRate, maxHealth, speed, id)
    {
    }

}
