using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DisplayDistanceText : MonoBehaviour
{
    [SerializeField] DistanceTracker_Module distanceTrackerModule;
    [SerializeField] TextMeshProUGUI text;

    private void Update()
    {
        text.text = Math.Round(distanceTrackerModule.travelledDistance).ToString();
    }
}
