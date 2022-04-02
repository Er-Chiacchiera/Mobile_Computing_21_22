using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUnlock : PowerUp
{
    public Button firstWeapon;
    public Button secondWeapon;

    private bool firstUnlock = false;
    private bool secondUnlock = false;

    public override void interaction(Collider2D collision)
    {
        if (!firstUnlock)
        {
            firstWeapon.interactable = true;
            firstUnlock = true;
        }

        if (!secondUnlock)
        {
            secondWeapon.interactable = true;
            secondUnlock = true;
        }
    }
}
