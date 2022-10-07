using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodEventManager : MonoBehaviour
{
    [SerializeField] GameObject bounce, crash, launch, rocket, chili, mitosis;
    
    FMODUnity.StudioEventEmitter bounceEmitter, crashEmitter, launchEmitter, rocketEmitter, chiliEmitter, mitosisEmitter;

    int skinID;

    FMOD.Studio.EventInstance music;
    // Start is called before the first frame update
    void Start()
    {
        bounceEmitter = bounce.GetComponent<FMODUnity.StudioEventEmitter>();
        crashEmitter = crash.GetComponent<FMODUnity.StudioEventEmitter>();
        launchEmitter = launch.GetComponent<FMODUnity.StudioEventEmitter>();
        rocketEmitter = rocket.GetComponent<FMODUnity.StudioEventEmitter>();
        chiliEmitter = chili.GetComponent<FMODUnity.StudioEventEmitter>();
        mitosisEmitter = mitosis.GetComponent<FMODUnity.StudioEventEmitter>();       
    }

    #region Eventos
    public void BounceSfx()
    {
        bounceEmitter.Play();     
    }

    public void CrashSfx()
    {
        crashEmitter.Play();
    }

    public void LaunchSfx()
    {
        launchEmitter.Play();
    }

    public void RocketSfx()
    {
        rocketEmitter.Play();
    }

    public void ChiliSfx()
    {
        chiliEmitter.Play();
    }

    public void MitosisSfx()
    {
        mitosisEmitter.Play();
    }
    public void PlayMusic()
    {
        skinID = (int)SkinData.ID;
        switch (skinID)
        {
            case 0:     //Base
                music = FMODUnity.RuntimeManager.CreateInstance("event:/Music-Ambience/BG_Music_Base");
                break;
            case 1:     //Kkwaii
                music = FMODUnity.RuntimeManager.CreateInstance("event:/Music-Ambience/BG_Music_Kkwaii");
                break;
            case 2:     //Shitsycal
                music = FMODUnity.RuntimeManager.CreateInstance("event:/Music-Ambience/BG_Music_Shitycal");
                break;
        }
        music.start();
    }
    public void StopMusic()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        music.release();
    }
    #endregion
}
