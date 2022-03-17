using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCircle : MonoBehaviour
{
    [SerializeField] private ParticleSystem fx;
    [SerializeField] private RectTransform fxHolder;
    [SerializeField] private Image circleImg;
    [SerializeField] private TextMeshProUGUI txtProgress;

    [SerializeField] [Range(0,1)] public float progress = 0f;

    private void Start()
    {
        fx.Play();
    }
    void Update()
    {
        circleImg.fillAmount = progress;
        txtProgress.text = Mathf.Floor(progress * 100).ToString();
        fxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -progress * 360));
    }
}
