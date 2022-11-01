using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMode_Module : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] DistanceTracker_Module distanceTracker;
    [SerializeField] int threshold;

    private void FixedUpdate()
    {
        if (distanceTracker.travelledDistance - distanceTracker.storedDistance > threshold)
        {
            Vector3 position = new Vector3(100,
                player.transform.position.y,
                player.transform.position.z
                );

            distanceTracker.storedDistance += threshold;
            player.transform.position = position;
        }
    }
}
