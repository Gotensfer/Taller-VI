using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop_Module : MonoBehaviour
{
    [SerializeField] private int bounces = 3, bouncesUsed = 0;
    private Rigidbody2D rb;
    private bool playerStopped = false;

    [SerializeField] PlayerEvents_Interface playerEvents;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        StopBounce();
    }

    public void StopBounce()
    {
        if (!playerStopped)
        {
            if (bouncesUsed == 1) playerEvents.BounceEvent.Invoke();

            bouncesUsed++;
            

            if (bouncesUsed >= bounces)
            {
                playerEvents.CrashEvent.Invoke();

                playerStopped = true;
                StopPlayer();
            }
        }
    }
    
    void StopPlayer()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
}
