using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCountTracker_Module : MonoBehaviour
{
    public int bouncesCountThisRun { get; private set; }
    public int launchesCountThisRun { get; private set; } // Medio innecesario ya que solo ocurre una vez por partida
    public int crashesCountThisRun { get; private set; } // Mismo asunto que arriba

    private void Start()
    {      
        bouncesCountThisRun = 0;
        launchesCountThisRun = 0;
        crashesCountThisRun = 0;
    }

    public void AddBounceCountToTracker()
    {
        bouncesCountThisRun++;
    }

    public void AddLaunchCountToTracker()
    {
        launchesCountThisRun++;
    }

    public void AddCrashCountToTracker()
    {
        crashesCountThisRun++;
    }

    // Subscribir al CrashEvent
    public void AddCollectedCountsToShenanigans()
    {
        ShenaniganData.bouncesCount += bouncesCountThisRun;
        ShenaniganData.launchesCount += launchesCountThisRun;
        ShenaniganData.crashesCount += crashesCountThisRun;

        PlayerPrefs.SetInt("launchesCount", ShenaniganData.bouncesCount);
        PlayerPrefs.SetInt("bouncesCount", ShenaniganData.launchesCount);
        PlayerPrefs.SetInt("crashesCount", ShenaniganData.crashesCount);
    }
}