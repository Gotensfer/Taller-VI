using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodAutomatizationManager : MonoBehaviour
{
    [Header("Manejo del Lowpass FMod")]
    [SerializeField] FMODUnity.StudioGlobalParameterTrigger auto;
    [SerializeField][Range(0, 6)] float altura;
    float alturaActual = 0;

    [Header("Valores altura")]
    [SerializeField] AltitudeTracker_Module altitude;
    [SerializeField] int altitudeMin;
    [SerializeField] int altitudeMax;



    private void Update()
    {
        if (altitudeMin - altitude.Altitude < 0)
        {
            if (altitudeMin - altitude.Altitude <= -33.3f && altitudeMin - altitude.Altitude >= -66.6f)
            {
                altura = Mathf.Lerp(altura = 0, altura = 1, 1f);
                alturaActual = 1;
            }
            else if (altitudeMin - altitude.Altitude <= -66.6f && altitudeMin - altitude.Altitude >= -99.9f)
            {
                altura = Mathf.Lerp(alturaActual, altura = 2, 1f);
                alturaActual = 2;
            }
            else if (altitudeMin - altitude.Altitude <= -99.9f && altitudeMin - altitude.Altitude >= -133.2f)
            {
                altura = Mathf.Lerp(alturaActual, altura = 3, 1f);
                alturaActual = 3;
            }
            else if (altitudeMin - altitude.Altitude <= -133.2f && altitudeMin - altitude.Altitude >= -166.5f)
            {
                altura = Mathf.Lerp(alturaActual, altura = 4, 1f);
                alturaActual = 4;
            }
            else if (altitudeMin - altitude.Altitude <= -166.5f && altitudeMin - altitude.Altitude >= -199.8f)
            {
                altura = Mathf.Lerp(alturaActual, altura = 5, 1f);
                alturaActual = 5;
            }
            else if (altitudeMin - altitude.Altitude <= -199.8f)
            {
                altura = Mathf.Lerp(alturaActual, altura = 6, 1f);
                alturaActual = 6;
            }
        }
        else
        {
            altura = 0;
        }

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Altura", altura);
    }
}

//300-