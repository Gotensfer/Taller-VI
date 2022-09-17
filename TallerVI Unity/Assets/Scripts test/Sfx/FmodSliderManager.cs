using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
public class FmodSliderManager : MonoBehaviour
{
    [SerializeField] Slider slider = null;
    [SerializeField] string busPath = "";
    FMOD.Studio.Bus bus;

    private void Start()
    {
        /*if (busPath != "")
        {
            
        }*/
        bus = FMODUnity.RuntimeManager.GetBus(busPath);

        bus.getVolume(out float volume);

        slider.value = volume * slider.maxValue;
    }

    public void SliderOutput()
    {
        if(slider != null)
        {
            bus.setVolume(slider.value / slider.maxValue);
        }
    }
}
