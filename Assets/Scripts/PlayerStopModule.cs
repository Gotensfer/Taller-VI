using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopModule : MonoBehaviour
{
    [SerializeField] private byte bounces = 3, bouncesUsed = 0;
    private Rigidbody2D rb;
    private bool playerStopped = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();//getplayer()
    }

    public void StopBounce()
    {
        if (!playerStopped)
        {
            bouncesUsed++;
            if (bouncesUsed >= bounces)
            {
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
