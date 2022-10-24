using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCountTracker_Module : MonoBehaviour
{
    public int bouncesCount { get; private set; }
    public int launchesCount { get; private set; } // Medio innecesario ya que solo ocurre una vez por partida
    public int crashesCount { get; private set; } // Mismo asunto que arriba
    
    public void AddBounceCountToTracker()
    {
        bouncesCount++;
    }

    public void AddLaunchCountToTracker()
    {
        launchesCount++;
    }

    public void AddCrashCountToTracker()
    {
        crashesCount++;
    }

    // Subscribir al CrashEvent
    public void AddCollectedCountsToShenanigans()
    {
        ShenaniganData.bouncesCount += bouncesCount;
        ShenaniganData.launchesCount += launchesCount;
        ShenaniganData.crashesCount += crashesCount;

        PlayerPrefs.SetInt("launchesCount", ShenaniganData.bouncesCount);
        PlayerPrefs.SetInt("bouncesCount", ShenaniganData.launchesCount);
        PlayerPrefs.SetInt("crashesCount", ShenaniganData.crashesCount);
    }
}