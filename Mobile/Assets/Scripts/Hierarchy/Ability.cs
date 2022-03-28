using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{

    //forceField parameter
    private bool isEnable = false;
    public bool isActive = false;
    [SerializeField] public GameObject ability;
    [SerializeField] private Button loadingButton;
    [SerializeField] private float countdown = 0;
    public float shutdown = 0;
    private float startingTime = 0;
    private LoadingCircle loadingCircle;



    // Start is called before the first frame update
    void Start()
    {
        loadingCircle = this.GetComponent<LoadingCircle>();
        startingTime = Time.fixedTime;
        loadingCircle.progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //loading
        if (!isEnable)
        {
            if (Time.fixedTime - startingTime > countdown)
            {
                isEnable = true;
                loadingButton.interactable = true;

            }

            else
            {
                loadingCircle.progress = (Time.fixedTime - startingTime) / countdown;
            }

        }

        //shutingdown
        if (isActive)
        {
            loadingButton.interactable = false;
            loadingCircle.progress = 0;

            if (Time.fixedTime - startingTime > shutdown)
            {
                ability.SetActive(false);
                isActive = false;
                isEnable = false;
                startingTime = Time.fixedTime;

            }

        }
    }

    public void setStartingTime(float value) { startingTime = value; }
}
