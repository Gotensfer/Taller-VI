using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAltitude : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayAltitude;
    [SerializeField] AltitudeTracker_Module altitudeTracker_Module;

    private void Update()
    {
        displayAltitude.text = $"{(int)altitudeTracker_Module.Altitude}";
    }
}
