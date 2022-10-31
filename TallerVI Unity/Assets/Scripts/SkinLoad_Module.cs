using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoad_Module : MonoBehaviour
{
    // Esto debería hacerse de forma automática por medio de Resources.Load o algo similar+
    // esta implementación no es muy apropiada para quien implemente la UI
    // pero como Julian es tan grosero que la mame

    [SerializeField] AnimatorOverrideController kkawaiSkinOverride;
    [SerializeField] AnimatorOverrideController cacsycalSkinOverride;
    [SerializeField] AnimatorOverrideController cacowboySkinOverride;
    [SerializeField] AnimatorOverrideController cacalienSkinOverride;
    [SerializeField] AnimatorOverrideController cacanautSkinOverride;
    [SerializeField] AnimatorOverrideController cacarutoSkinOverride;
    [SerializeField] AnimatorOverrideController goldenSkinOverride;

    [SerializeField] VFXController_Module vfxController;


    private void Start()
    {
        SkinData.ID = (SkinID)PlayerPrefs.GetInt($"LastSelectedSkin");

        switch (SkinData.ID)
        {
            case SkinID.Base:
                vfxController.SetVFXForBaseCacaleta();
                break;
            case SkinID.Kkawai:
                GetComponent<Animator>().runtimeAnimatorController = kkawaiSkinOverride;
                vfxController.SetVFXForKkawai();
                break;
            case SkinID.Cacsycal:
                GetComponent<Animator>().runtimeAnimatorController = cacsycalSkinOverride;
                vfxController.SetVFXForShitsycal();
                break;
            case SkinID.Cacowboy:
                GetComponent<Animator>().runtimeAnimatorController = cacowboySkinOverride;
                vfxController.SetVFXForCacowboy();
                break;
            case SkinID.Cacalien:
                GetComponent<Animator>().runtimeAnimatorController = cacalienSkinOverride;
                vfxController.SetVFXForCacalien();
                break;
            case SkinID.Cacanaut:
                GetComponent<Animator>().runtimeAnimatorController = cacanautSkinOverride;
                vfxController.SetVFXForCacanaut();
                break;
            case SkinID.Cacaruto:
                GetComponent<Animator>().runtimeAnimatorController = cacarutoSkinOverride;
                vfxController.SetVFXForCacaruto();
                break;
            case SkinID.Golden:
                GetComponent<Animator>().runtimeAnimatorController = goldenSkinOverride;
                vfxController.SetVFXForGolden();
                break;
            default:
                Debug.LogError("No se inicializo la skin o no se encontro?");
                break;
        }
    }
}
