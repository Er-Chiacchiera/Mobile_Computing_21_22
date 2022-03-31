using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenu : MonoBehaviour
{

    public void Weapon1()
    {
        FindObjectOfType<Shooting>().SetIdWeapon(1);
        GameObject.Find("Robot").GetComponent<Animator>().SetTrigger("disable");
        GameObject.Find("Robot").GetComponent<Animator>().SetTrigger("w1");
    }

    public void Weapon2()
    {
        FindObjectOfType<Shooting>().SetIdWeapon(2);
        GameObject.Find("Robot").GetComponent<Animator>().SetTrigger("disable");
        GameObject.Find("Robot").GetComponent<Animator>().SetTrigger("w2");
    }

    public void Weapon3()
    {
        FindObjectOfType<Shooting>().SetIdWeapon(3);
        GameObject.Find("Robot").GetComponent<Animator>().SetTrigger("disable");
        GameObject.Find("Robot").GetComponent<Animator>().SetTrigger("w3");
    }
}
