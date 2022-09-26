using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController_Module : MonoBehaviour
{
    [Header("Base trail system")]
    [SerializeField] ParticleSystem basePS;
    [SerializeField] TrailRenderer baseTrail;

    [Header("Chilli trail system")]
    [SerializeField] ParticleSystem chilliPS;
    [SerializeField] TrailRenderer fireTrail;
    [SerializeField] ParticleSystem smokePS;

    [Header("Rocket explotion system")]
    [SerializeField] ParticleSystem rocketExplotion;

    [Header("Pidgeon end system")]
    [SerializeField] ParticleSystem pidgeonEnd;

    [Header("Mitosis end system")]
    [SerializeField] ParticleSystem mitosisEnd;

    [Header("Fecalito start system")]
    [SerializeField] ParticleSystem fecalitoStart;

    private void Start()
    {
        DeactivateChilliTrail();
    }

    #region"Métodos internos para manipular los PS y Trails
    void ActivateChilliTrail()
    {
        chilliPS.Play();
        fireTrail.emitting = true;
        smokePS.Play();
    }

    void DeactivateChilliTrail()
    {
        chilliPS.Stop();
        fireTrail.emitting = false;
        smokePS.Stop();
    }

    void ActivateBaseTrail()
    {
        basePS.Play();
        baseTrail.emitting = true;
    }

    void DeactivateBaseTrail()
    {
        basePS.Stop();
        baseTrail.emitting = false;
    }
    #endregion

    #region"Métodos publicos para eventos"
    public void SetChilliTrail_VFX()
    {
        ActivateChilliTrail();
        DeactivateBaseTrail();
    }

    public void SetBaseTrail_VFX()
    {
        ActivateBaseTrail();
        DeactivateChilliTrail();
    }

    public void SetRocketExplotion_VFX()
    {
        rocketExplotion.Play(true);
    }

    public void SetPidgeonEnd_VFX()
    {
        pidgeonEnd.Play(true);
    }

    public void SetMitosis_VFX()
    {
        mitosisEnd.Play(true);
    }

    public void SetFecalito_VFX()
    {
        fecalitoStart.Play(true);
    }
    #endregion
}
