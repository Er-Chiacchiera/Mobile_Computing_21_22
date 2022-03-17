using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingButton : MonoBehaviour
{
    [SerializeField] private GameObject forceField;

    [SerializeField] private GameObject robot;

    public void ForceField()
    {
        forceField.SetActive(true);
        robot.GetComponent<Player>().startingTime = Time.fixedTime;
        robot.GetComponent<Player>().isActive =true;

    }
}
