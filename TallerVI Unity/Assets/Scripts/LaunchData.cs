using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LaunchData
{
    public static float maxVelocity;
    public static int anglePerSecond;
    public static float impulse;
    public static int bounces;

    public static void ResetLaunchData()
    {
        maxVelocity = 15;
        anglePerSecond = 50;
        impulse = 5;
        bounces = 1;
    }
}