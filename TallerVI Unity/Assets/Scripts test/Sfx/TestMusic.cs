using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusic : MonoBehaviour
{
    [SerializeField] GameObject musicKKwaii, musicBase;
    FMODUnity.StudioEventEmitter musicKkwaiiEmitter, musicBaseEmitter;
    int skinID = 0;   

    FMOD.Studio.EventInstance music;

    // Start is called before the first frame update
    void Start()
    {
        musicKkwaiiEmitter = musicKKwaii.GetComponent<FMODUnity.StudioEventEmitter>();
        musicBaseEmitter = musicBase.GetComponent<FMODUnity.StudioEventEmitter>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            /*music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            music = FMODUnity.RuntimeManager.CreateInstance("event:/Music-Ambience/BG_Music_Base");
            music.start();*/
            print("a");
            skinID = (int)SkinData.ID;
            switch (skinID)
            {
                case 0:
                    //base
                    break;
                case 1:
                    //kkwai
                    break;
            }
            musicBaseEmitter.Play();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            /*music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            music = FMODUnity.RuntimeManager.CreateInstance("event:/Music-Ambience/BG_Music_Kkwaii");
            music.start();*/
            print("b");
        }
    }
}
