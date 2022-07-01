using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject buttonsController;
    public GameObject options;
    public GameObject credits;


    public void PlayButton()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OptionButton()
    {
        buttonsController.SetActive(false);
        options.SetActive(true);
    }

    public void CreditsButton()
    {
        buttonsController.SetActive(false);
        credits.SetActive(true);
    }

    public void BackFromOptions()
    {
        buttonsController.SetActive(true);
        options.SetActive(false);
    }

    public void BackFromCredits()
    {
        buttonsController.SetActive(true);
        credits.SetActive(false);
    }

}
