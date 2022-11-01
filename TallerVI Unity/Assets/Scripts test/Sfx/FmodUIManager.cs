using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodUIManager : MonoBehaviour
{
    [SerializeField] GameObject sfx;
    
    FMODUnity.StudioEventEmitter sfxEmitter;

    private void Start()
    {
        sfxEmitter = sfx.GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public void StartSFX()
    {
        sfxEmitter.Play();
    }
}
