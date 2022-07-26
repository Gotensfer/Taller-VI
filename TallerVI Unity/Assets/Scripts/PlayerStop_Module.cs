using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop_Module : MonoBehaviour
{
    [SerializeField] public int bounces = 2, bouncesUsed = 0;
    private Rigidbody2D rb;
    private bool playerStopped = false;

    [SerializeField] PlayerEvents_Interface playerEvents;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bounces += LaunchData.bounces; // El sistema exige esta forma de aplicar la cantidad de rebotes
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        StopBounce();
    }

    public void StopBounce()
    {
        if (!playerStopped)
        {
            bouncesUsed++;

            if (bouncesUsed < bounces && bouncesUsed != 1) playerEvents.BounceEvent.Invoke();

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
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }
}
