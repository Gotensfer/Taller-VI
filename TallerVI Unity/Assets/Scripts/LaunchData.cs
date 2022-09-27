using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LaunchData
{
    public static float maxVelocity;
    public static int anglePerSecond;
    public static float impulse;
    public static int bounces;

    // Debería ser un ScriptableObject
    public static float min_MaxVelocity = 5;
    public static int min_AnglePerSecond = 50;
    public static float min_Impulse = 5;
    public static int min_bounces = 1;

    public static void ResetLaunchData()
    {
        maxVelocity = min_MaxVelocity;
        anglePerSecond = min_AnglePerSecond;
        impulse = min_Impulse;
        bounces = min_bounces;
    }
}