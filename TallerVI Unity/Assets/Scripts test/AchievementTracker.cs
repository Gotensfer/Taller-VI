using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTracker : MonoBehaviour
{
    [SerializeField] DistanceTracker_Module distanceTracker_Module;
    [SerializeField] AltitudeTracker_Module altitudeTracker_Module;

    float maxAltitude;
    float maxDistance;

    private void FixedUpdate()
    {
        if (distanceTracker_Module.travelledDistance > maxDistance)
        {
            maxDistance = distanceTracker_Module.travelledDistance;
        }

        if (altitudeTracker_Module.Altitude > maxAltitude)
        {
            maxAltitude = altitudeTracker_Module.Altitude;
        }
    }

    public void CheckAchievements()
    {
        if (maxAltitude > 500) PlayerPrefs.SetInt("Achievement5", 1);
        if (maxDistance > 2000) PlayerPrefs.SetInt("Achievement1", 1);
        if (maxDistance > 4000) PlayerPrefs.SetInt("Achievement2", 1);
        if (maxDistance > 6000) PlayerPrefs.SetInt("Achievement3", 1);
        if (maxDistance > 8000) PlayerPrefs.SetInt("Achievement4", 1);

        PlayerPrefs.Save();

    }
}
