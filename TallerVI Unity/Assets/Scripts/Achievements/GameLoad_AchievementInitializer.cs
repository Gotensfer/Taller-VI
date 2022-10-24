using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad_AchievementInitializer : MonoBehaviour
{
    [SerializeField] Achievement[] trackedAchievements;

    private void Start()
    {
        int len = trackedAchievements.Length;

        for (int i = 0; i < len; i++)
        {
            if (PlayerPrefs.GetInt(trackedAchievements[i]._name, -1) == -1)
            {
                PlayerPrefs.SetInt(trackedAchievements[i]._name, 0);   
            }
        }

        // Estos stats nunca deberían cambiar, NUNCAAAAAA
        if (PlayerPrefs.GetInt("collectedPidgeons", -1) == -1)
        {
            PlayerPrefs.SetInt("collectedPidgeons", 0);
            PlayerPrefs.SetInt("collectedRockets", 0);
            PlayerPrefs.SetInt("collectedChillis", 0);
            PlayerPrefs.SetInt("launchesCount", 0);
            PlayerPrefs.SetInt("bouncesCount", 0);
            PlayerPrefs.SetInt("crashesCount", 0);
        }
        else
        {
            ShenaniganData.collectedPidgeons = PlayerPrefs.GetInt("collectedPidgeons");
            ShenaniganData.collectedRockets = PlayerPrefs.GetInt("collectedPidgeons");
            ShenaniganData.collectedChillis = PlayerPrefs.GetInt("collectedPidgeons");
            ShenaniganData.launchesCount = PlayerPrefs.GetInt("collectedPidgeons");
            ShenaniganData.bouncesCount = PlayerPrefs.GetInt("collectedPidgeons");
            ShenaniganData.crashesCount = PlayerPrefs.GetInt("collectedPidgeons");
        }
    }
}
