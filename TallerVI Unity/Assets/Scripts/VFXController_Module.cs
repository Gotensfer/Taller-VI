using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController_Module : MonoBehaviour
{
    // Ohno! Public madness!
    // Ciertamente, uno de los peores c�digos que he hecho - JF

    [Header("Base trail system")]

    public ParticleSystem BasePSReference;
    public TrailRenderer BaseTrailReference;

    #region"Campos para asignar los PS y Trails en el inspector"
    // - Base
    [SerializeField] ParticleSystem basePS;
    [SerializeField] TrailRenderer baseTrail;

    // - Kkawai
    [SerializeField] ParticleSystem Kkawai_basePS;
    [SerializeField] TrailRenderer Kkawai_baseTrail;

    // - Cacowboy
    [SerializeField] ParticleSystem Cacowboy_basePS;
    [SerializeField] TrailRenderer Cacowboy_baseTrail;

    // - Shitcycal
    [SerializeField] ParticleSystem Shitsycal_basePS;
    [SerializeField] TrailRenderer Shitsycal_baseTrail;

    // - Cacanaut
    [SerializeField] ParticleSystem Cacanaut_basePS;
    [SerializeField] TrailRenderer Cacanaut_baseTrail;

    // - Cacalien
    [SerializeField] ParticleSystem Cacalien_basePS;
    [SerializeField] TrailRenderer Cacalien_baseTrail;

    // - Cacaruto
    [SerializeField] ParticleSystem Cacaruto_basePS;
    [SerializeField] TrailRenderer Cacaruto_baseTrail;

    // - Golden
    [SerializeField] ParticleSystem Golden_basePS;
    [SerializeField] TrailRenderer Golden_baseTrail;

    // - 20s
    [SerializeField] ParticleSystem c20s_basePS;
    [SerializeField] TrailRenderer c20s_baseTrail;
    #endregion

    [Header("Chilli trail system")]
    [SerializeField] ParticleSystem chilliPS;
    [SerializeField] TrailRenderer fireTrail;
    [SerializeField] ParticleSystem smokePS;
    [SerializeField] ParticleSystem smokePS2;

    [Header("Rocket explotion system")]
    [SerializeField] ParticleSystem rocketExplotion;
    [SerializeField] TrailRenderer rocketTrail;

    [Header("Pidgeon end system")]
    [SerializeField] ParticleSystem pidgeonEnd;

    [Header("Mitosis end system")]
    [SerializeField] ParticleSystem mitosisEnd_Reference;
    #region"Campos para asignar los PS de Mitosis legs en el inspector"
    [SerializeField] ParticleSystem mitosisEnd_Base; // Base
    [SerializeField] ParticleSystem mitosisEnd_Kkawai; // Cakawaii
    [SerializeField] ParticleSystem mitosisEnd_Shitsycal; // Cacsycal
    [SerializeField] ParticleSystem mitosisEnd_Cacowboy; // Cacavaquera
    [SerializeField] ParticleSystem mitosisEnd_Cacalien; // Cacalien
    [SerializeField] ParticleSystem mitosisEnd_Cacanaut; // Cacanauta
    [SerializeField] ParticleSystem mitosisEnd_Cacaruto; // Cacaruto
    [SerializeField] ParticleSystem mitosisEnd_Golden; // Golden
    [SerializeField] ParticleSystem mitosisEnd_c20s; // c20s
    #endregion

    [Header("Fecalito start system")]
    [SerializeField] ParticleSystem fecalitoStart;

    [Header("Bounce particle systems")]
    [SerializeField] ParticleSystem bouncePSReference;
    #region"Campos para asignar los PS de Bounce en el inspector"
    // - Base
    [SerializeField] ParticleSystem base_bouncePS;

    // - Kkawai
    [SerializeField] ParticleSystem Kkawai_bouncePS;

    // - Cacowboy
    [SerializeField] ParticleSystem Cacowboy_bouncePS;

    // - Shitcycal
    [SerializeField] ParticleSystem Shitsycal_bouncePS;

    // - Cacanaut
    [SerializeField] ParticleSystem Cacanaut_bouncePS;

    // - Cacalien
    [SerializeField] ParticleSystem Cacalien_bouncePS;

    // - Cacaruto
    [SerializeField] ParticleSystem Cacaruto_bouncePS;

    // - Golden
    [SerializeField] ParticleSystem Golden_bouncePS;

    // - c20s
    [SerializeField] ParticleSystem c20s_bouncePS;
    #endregion

    private void Start()
    {
        DeactivateChilliTrail();
        DeactivateRocketTrail();
    }

    #region"M�todos internos para manipular los PS y Trails
    void ActivateChilliTrail()
    {
        chilliPS.Play();
        fireTrail.emitting = true;
        smokePS.Play();
        smokePS2.Play();
    }

    void DeactivateChilliTrail()
    {
        chilliPS.Stop();
        fireTrail.emitting = false;
        smokePS.Stop();
        smokePS2.Stop();
    }

    void ActivateBaseTrail()
    {
        BasePSReference.Play();
        BaseTrailReference.emitting = true;
    }

    void DeactivateBaseTrail()
    {
        BasePSReference.Stop();
        BaseTrailReference.emitting = false;
    }
    #endregion

    #region"M�todos publicos para eventos"
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

    public void SetBounce_VFX()
    {
        bouncePSReference.Play(true);
    }

    public void SetRocketExplotion_VFX()
    {
        rocketExplotion.Play(true);
    }

    public void SetRocketTrail()
    {
        rocketTrail.emitting = true;
    }

    public void DeactivateRocketTrail()
    {
        rocketTrail.emitting = false;
    }

    public void SetPidgeonEnd_VFX()
    {
        pidgeonEnd.Play(true);
    }

    public void SetMitosis_VFX()
    {
        mitosisEnd_Reference.Play(true);
    }

    public void SetFecalito_VFX()
    {
        fecalitoStart.Play(true);
    }
    #endregion

    #region"Inicializadores de VFX con Skins"
    public void SetVFXForBaseCacaleta()
    {
        BasePSReference = basePS;
        BaseTrailReference = baseTrail;

        bouncePSReference = base_bouncePS;
        mitosisEnd_Reference = mitosisEnd_Base;

        DeActivateAllVFX();
        ActivateSelectedTrailVFX();
        ActivateBaseTrail();
    }

    public void SetVFXForKkawai()
    {
        BasePSReference = Kkawai_basePS;
        BaseTrailReference = Kkawai_baseTrail;

        bouncePSReference = Kkawai_bouncePS;
        mitosisEnd_Reference = mitosisEnd_Kkawai;

        DeActivateAllVFX();
        ActivateSelectedTrailVFX();
        ActivateBaseTrail();
    }

    public void SetVFXForCacowboy()
    {
        BasePSReference = Cacowboy_basePS;
        BaseTrailReference = Cacowboy_baseTrail;

        bouncePSReference = base_bouncePS;
        mitosisEnd_Reference = mitosisEnd_Cacowboy;

        DeActivateAllVFX();
        ActivateSelectedTrailVFX();
        ActivateBaseTrail();
    }

    public void SetVFXForCacalien()
    {
        BasePSReference = Cacalien_basePS;
        BaseTrailReference = Cacalien_baseTrail;

        bouncePSReference = Cacalien_bouncePS;
        mitosisEnd_Reference = mitosisEnd_Cacalien;

        DeActivateAllVFX();
        ActivateSelectedTrailVFX();
        ActivateBaseTrail();
    }

    public void SetVFXForCacanaut()
    {
        BasePSReference = Cacanaut_basePS;
        BaseTrailReference = Cacanaut_baseTrail;

        bouncePSReference = base_bouncePS;
        mitosisEnd_Reference = mitosisEnd_Cacanaut;

        DeActivateAllVFX();
        ActivateSelectedTrailVFX();
        ActivateBaseTrail();
    }

    public void SetVFXForShitsycal()
    {
        BasePSReference = Shitsycal_basePS;
        BaseTrailReference = Shitsycal_baseTrail;

        bouncePSReference = Shitsycal_bouncePS;
        mitosisEnd_Reference = mitosisEnd_Shitsycal;

        DeActivateAllVFX();
        ActivateSelectedTrailVFX();
        ActivateBaseTrail();
    }

    public void SetVFXForCacaruto()
    {
        BasePSReference = Cacaruto_basePS;
        BaseTrailReference = Cacaruto_baseTrail;

        bouncePSReference = Cacaruto_bouncePS;
        mitosisEnd_Reference = mitosisEnd_Cacaruto;

        DeActivateAllVFX();
        ActivateSelectedTrailVFX();
        ActivateBaseTrail();
    }

    public void SetVFXForGolden()
    {
        BasePSReference = Golden_basePS;
        BaseTrailReference = Golden_baseTrail;

        bouncePSReference = Golden_bouncePS;
        mitosisEnd_Reference = mitosisEnd_Golden;

        DeActivateAllVFX();
        ActivateSelectedTrailVFX();
        ActivateBaseTrail();
    }

    public void SetVFXForC20s()
    {
        BasePSReference = c20s_basePS;
        BaseTrailReference = c20s_baseTrail;

        bouncePSReference = c20s_bouncePS;
        mitosisEnd_Reference = mitosisEnd_c20s;

        DeActivateAllVFX();
        ActivateSelectedTrailVFX();
        ActivateBaseTrail();
    }

    [SerializeField] Transform trailContainerObject;
    [SerializeField] Transform bounceContainerObject;
    [SerializeField] Transform mitosisEndContainerObject;
    void DeActivateAllVFX()
    {
        int len = trailContainerObject.childCount;
        for (int i = 0; i < len; i++)
        {
            trailContainerObject.GetChild(i).gameObject.SetActive(false);
        }

        len = bounceContainerObject.childCount;
        for (int i = 0; i < len; i++)
        {
            bounceContainerObject.GetChild(i).gameObject.SetActive(false);
        }

        len = mitosisEndContainerObject.childCount;
        for (int i = 0; i < len; i++)
        {
            mitosisEndContainerObject.GetChild(i).gameObject.SetActive(false);
        }
    }

    void ActivateSelectedTrailVFX()
    {
        BasePSReference.gameObject.SetActive(true);
        BaseTrailReference.gameObject.SetActive(true);
        bouncePSReference.gameObject.SetActive(true);
        mitosisEnd_Reference.gameObject.SetActive(true);
    }
    #endregion
}
