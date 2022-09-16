using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents_Interface : MonoBehaviour
{
    [SerializeField] bool debuggingActivated;

    [Header("General Events")]
    public UnityEvent LaunchEvent;
    public UnityEvent BounceEvent;
    public UnityEvent CrashEvent;
    public UnityEvent PoweredUpEvent;
    public UnityEvent PoweredDownEvent;

    [Header("Fly direction Events")]
    public UnityEvent AscendingEvent;
    public UnityEvent GlidingEvent;
    public UnityEvent FallingEvent;

    [Header("Specific power up Events")]
    public UnityEvent ChilliEvent;
    public UnityEvent PidgeonEvent;
    public UnityEvent RocketEvent;
    public UnityEvent MitosisEvent;
    public UnityEvent MitosisPickUpEvent;
    public UnityEvent FecalitoEvent;

    private void Start()
    {
#if UNITY_EDITOR
        if (debuggingActivated)
        {
            LaunchEvent.AddListener(DebugLaunchEvent);
            BounceEvent.AddListener(DebugBounceEvent);
            CrashEvent.AddListener(DebugCrashEvent);
            PoweredUpEvent.AddListener(DebugPoweredUpEvent);
            PoweredDownEvent.AddListener(DebugPoweredDownEvent);
            AscendingEvent.AddListener(DebugAscendingEvent);
            GlidingEvent.AddListener(DebugGlidingEvent);
            FallingEvent.AddListener(DebugFallingEvent);
            ChilliEvent.AddListener(DebugChilliEvent);
            PidgeonEvent.AddListener(DebugPidgeonEvent);
            RocketEvent.AddListener(DebugRocketEvent);
            MitosisEvent.AddListener(DebugMitosisEvent);
            MitosisPickUpEvent.AddListener(DebugMitosisPickUpEvent);

        }
#endif
    }

#if UNITY_EDITOR
    #region"Debug methods"
    public void DebugLaunchEvent()
    {
        print("Launch event fired");
    }

    public void DebugBounceEvent()
    {
        print("Bounce event fired");
    }

    public void DebugCrashEvent()
    {
        print("Crash event fired");
    }

    public void DebugPoweredUpEvent()
    {
        print("PoweredUp event fired");
    }

    public void DebugPoweredDownEvent()
    {
        print("PoweredDown event fired");
    }

    public void DebugAscendingEvent()
    {
        print("Ascending event fired");
    }

    public void DebugGlidingEvent()
    {
        print("Gliding event fired");
    }

    public void DebugFallingEvent()
    {
        print("Falling event fired");
    }

    public void DebugChilliEvent()
    {
        print("Chilli event fired");
    }

    public void DebugPidgeonEvent()
    {
        print("Pidgeon event fired");
    }

    public void DebugRocketEvent()
    {
        print("Rocket event fired");
    }

    public void DebugMitosisEvent()
    {
        print("Mitosis event fired");
    }

    public void DebugMitosisPickUpEvent()
    {
        print("Mitosis pick up event fired");
    }
    #endregion
#endif
}
