using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodAnimEvents : MonoBehaviour
{
    [SerializeField] GameObject pidgeon;
    FMODUnity.StudioEventEmitter pidgeonEmitter;
    private void Start()
    {
        pidgeonEmitter = pidgeon.GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public void PidgeonFlutter()
    {
        pidgeonEmitter.Play();
    }

    public void StopFlutter()
    {
        pidgeonEmitter.Stop();
    }
}
