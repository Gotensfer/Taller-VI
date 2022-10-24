using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedCountTracker_Module : MonoBehaviour
{
    public int pidgeonsThisRun { private set; get; }
    public int chillisThisRun { private set; get; }
    public int rocketsThisRun { private set; get; }

    private void Start()
    {
        pidgeonsThisRun = 0;
        chillisThisRun = 0;
        rocketsThisRun = 0;
    }

    // Estos 3 métodos deben subscribirse a sus eventos de powerup correspondientes

    public void AddPidgeonCountToTracker()
    {
        pidgeonsThisRun++;
    }

    public void AddChilliCountToTracker()
    {
        chillisThisRun++;
    }

    public void AddRocketCountToTracker()
    {
        rocketsThisRun++;
    }

    // Subscribir al CrashEvent
    public void AddCollectedCountsToShenanigans()
    {
        ShenaniganData.collectedPidgeons += pidgeonsThisRun;
        ShenaniganData.collectedChillis += chillisThisRun;
        ShenaniganData.collectedRockets += rocketsThisRun;

        PlayerPrefs.SetInt("collectedPidgeons", ShenaniganData.collectedPidgeons);
        PlayerPrefs.SetInt("collectedChillis", ShenaniganData.collectedChillis);
        PlayerPrefs.SetInt("collectedRockets", ShenaniganData.collectedRockets);
    }
}
