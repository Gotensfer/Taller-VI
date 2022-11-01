using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame_Module : MonoBehaviour
{
    [SerializeField] PlayerEvents_Interface playerEvents;
    [SerializeField] DistanceTracker_Module distanceTracker;
    [SerializeField] Rigidbody2D rb;

    bool reachedEndGameFlag = false;
    bool onEndGame = false;

    private void FixedUpdate()
    {
        if (!reachedEndGameFlag)
        {
            if (distanceTracker.travelledDistance >= 10000)
            {
                reachedEndGameFlag = true;

                if (PlayerPrefs.GetInt("FirstTimeEndGame", -1) != 1)
                {
                    // Ganar

                    playerEvents.CrashEvent.Invoke();
                    onEndGame = true;

                    PlayerPrefs.SetInt("FirstTimeEndGame", 1);
                }                       
            }
        }

        if (onEndGame)
        {
            rb.velocity = Vector2.zero;
        }
        
    }
}
