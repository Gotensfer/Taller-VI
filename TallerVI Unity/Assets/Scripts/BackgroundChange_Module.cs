using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum World
{
    City = 0,
    Forest = 1,
    Desert = 2
}

public class BackgroundChange_Module : MonoBehaviour
{
    [SerializeField] DistanceTracker_Module distanceTrackerModule;

    [SerializeField] int firstWorldThreshold;
    [SerializeField] int secondWorldThreshold;
    // [SerializeField] int thirdWordlThreshold; // Aun no tenemos límite para el modo infinito, pero sería 10k

    [SerializeField] TextureChanger[] textureChangers;

    World world;

    private void FixedUpdate()
    {
        switch (world)
        {
            case World.City:
                if (distanceTrackerModule.travelledDistance >= firstWorldThreshold)
                {
                    int len = textureChangers.Length;
                    for (int i = 0; i < len; i++)
                    {
                        textureChangers[i].ChangeCondition1();
                    }

                    world = World.Forest;
                }

                break;

            case World.Forest:
                if (distanceTrackerModule.travelledDistance >= secondWorldThreshold)
                {
                    int len = textureChangers.Length;
                    for (int i = 0; i < len; i++)
                    {
                        textureChangers[i].ChangeCondition2();
                    }

                    world = World.Desert;
                }

                break;

            case World.Desert:
                break;
        }
    }
}