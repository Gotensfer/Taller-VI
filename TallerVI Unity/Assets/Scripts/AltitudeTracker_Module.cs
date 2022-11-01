using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltitudeTracker_Module : MonoBehaviour
{
    [SerializeField] Transform player;

    float altitudeOffset;
    float altitude;
    public float Altitude { get => altitude; }

    float maxAltitude;
    public float MaxAltitude { get => maxAltitude; }

    private void Start()
    {
        altitudeOffset = player.position.y;
    }

#if UNITY_EDITOR
    [SerializeField] bool debugMode;
#endif
    private void FixedUpdate()
    {
        altitude = player.position.y - altitudeOffset;

        if (altitude < 1) altitude = 0;

        if (altitude > maxAltitude) maxAltitude = altitude;

#if UNITY_EDITOR
        if (debugMode)
        {
            print($"Altitude: {Altitude}");
        }
#endif
    }
}