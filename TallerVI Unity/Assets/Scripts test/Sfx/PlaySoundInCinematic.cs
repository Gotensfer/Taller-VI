using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Video;

public class PlaySoundInCinematic : MonoBehaviour
{
    [SerializeField] private VideoPlayer cinematic;

    private bool flag = false;

    void Update()
    {
        if (!flag)
        {
            Invoke("PlayVideo", 0.05f);
            flag = true;
        }
    }

    void PlaySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Cinematica/AudioCinematica");
    }

    void PlayVideo()
    {
        cinematic.Play();
        PlaySound();
    }
}
