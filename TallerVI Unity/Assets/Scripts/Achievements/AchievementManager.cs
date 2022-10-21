using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementManager : MonoBehaviour
{
    public Achievement[] trackedAchievements;

    [SerializeField] DistanceTracker_Module distanceTracker_Module;
    [SerializeField] AltitudeTracker_Module altitudeTracker_Module;
    // Tracker de coger
    // Tracker de eventos

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckAchievements();
        }
    }

    public void CheckAchievements()
    {
        int len = trackedAchievements.Length;
        int trackedDistance = (int)distanceTracker_Module.travelledDistance;
        int trackedMaxAltitude = (int)altitudeTracker_Module.MaxAltitude;


        float startTime = Time.realtimeSinceStartup;

        for (int i = 0; i < len; i++)
        {
            if (PlayerPrefs.GetInt($"{trackedAchievements[i]._name}") == 1) continue;

            switch (trackedAchievements[i]) // No esperaba que esto funcionará sin tanta dificultad -JF
            {
                case DistanceAchievement:
                    // Esto probablemente es una terriiiible idea: 
                    // esto generará un potencial memory leak al realizar esta operación en cada caso -JF
                    DistanceAchievement distanceAchievement = trackedAchievements[i] as DistanceAchievement;
                    
                    if (trackedDistance >= distanceAchievement.distanceNeededForAchievement)
                    {
                        print($"Got achievement {distanceAchievement._name}");
                        // Funcionalidad
                    }

                    break;

                case AltitudeAchievement:
                    AltitudeAchievement altitudeAchievement = trackedAchievements[i] as AltitudeAchievement;

                    if (trackedMaxAltitude >= altitudeAchievement.altitudeNeededForAchievement)
                    {
                        print($"Got achievement {altitudeAchievement._name}");
                        // Funcionalidad
                    }

                    break;

                case CollectAchievement:
                    print("This is a collect achivement");
                    break;

                case EventAchievement:
                    print("this is an event achievement");
                    break;
            }
        }

        print($"Achievement manager took {Math.Round(startTime - Time.realtimeSinceStartup, 2)}s to finish");
    }
}
