using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStates
{
    launch,
    standardFlying,
    poweredUpFlying,
    crashed
}

public enum FlyingStates
{
    launching,
    ascending,
    gliding,
    falling,
    none
}

public class FlyingStates_Module : MonoBehaviour
{
    PlayerStates playerState;
    public PlayerStates PlayerState { get => playerState; set => playerState = value; }
    
    FlyingStates flyingState;
    public FlyingStates FlyingState { get => flyingState; set => flyingState = value; }

    [SerializeField] PlayerEvents_Interface playerEvents;

    [SerializeField] float glidingMinYVelocityThreshold;
    [SerializeField] float glidingMaxYVelocityThreshold;
    Rigidbody2D rb;

    private void Start()
    {
        PlayerState = PlayerStates.launch;
        FlyingState = FlyingStates.launching;
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetStandard()
    {
        playerState = PlayerStates.standardFlying;
    }

    public void SetPoweredUp()
    {
        playerState = PlayerStates.poweredUpFlying;
    }

    public void SetCrashed()
    {
        playerState = PlayerStates.crashed;
    }

    private void FixedUpdate()
    {
        if (playerState == PlayerStates.poweredUpFlying || playerState == PlayerStates.crashed)
        {
            flyingState = FlyingStates.none;
            return;
        }

        if (rb.velocity.y > glidingMaxYVelocityThreshold && flyingState != FlyingStates.ascending)
        {
            flyingState = FlyingStates.ascending;
            playerEvents.AscendingEvent.Invoke();
        }
        else if (rb.velocity.y < glidingMinYVelocityThreshold && flyingState != FlyingStates.falling)
        {
            flyingState = FlyingStates.falling;
            playerEvents.FallingEvent.Invoke();
        }
        else if (rb.velocity.y > glidingMinYVelocityThreshold && rb.velocity.y < glidingMaxYVelocityThreshold && flyingState != FlyingStates.gliding)
        {
            if (rb.velocity.y > -0.01f && rb.velocity.y < 0.01f) return; // Evitar un glide accidental al inicio del juego
            flyingState = FlyingStates.gliding;
            playerEvents.GlidingEvent.Invoke();
        }
    }
}
