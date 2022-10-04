using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    Slider progressSlider;
    [SerializeField] DistanceTracker_Module distanceTracker_Module;

    [SerializeField] Slider slider;

    private void Start()
    {
        progressSlider = GetComponent<Slider>();

        float fillAmount = Mathf.Clamp(PlayerPrefs.GetFloat("Distance", 0) / 10000f, 0.035f, 1);
        slider.value = fillAmount;

    }

    private void FixedUpdate()
    {
        float fillAmount = Mathf.Clamp(distanceTracker_Module.travelledDistance / 10000f, 0.035f, 1);
        progressSlider.value = fillAmount;
    }
}
