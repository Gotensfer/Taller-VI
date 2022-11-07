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
    [SerializeField] AnimatorOverrideController c20sOverride;

    [SerializeField] VFXController_Module vfxController;

    [SerializeField] SpriteRenderer toilet;

    [SerializeField] Sprite ckawaiToilet;
    [SerializeField] Sprite crapsycalToilet;
    [SerializeField] Sprite cacowboyToilet;
    [SerializeField] Sprite cacalienToilet;
    [SerializeField] Sprite cacanautToilet;
    [SerializeField] Sprite cacarutoToilet;
    [SerializeField] Sprite goldenToilet;
    [SerializeField] Sprite c20sToilet;

    [SerializeField] GameObject c20sVolume;
    [SerializeField] Transform parentForInstantiatedObjects;

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
                toilet.sprite = ckawaiToilet;
                break;
            case SkinID.Cacsycal:
                GetComponent<Animator>().runtimeAnimatorController = cacsycalSkinOverride;
                vfxController.SetVFXForShitsycal();
                toilet.sprite = crapsycalToilet;
                break;
            case SkinID.Cacowboy:
                GetComponent<Animator>().runtimeAnimatorController = cacowboySkinOverride;
                vfxController.SetVFXForCacowboy();
                toilet.sprite = cacowboyToilet;
                break;
            case SkinID.Cacalien:
                GetComponent<Animator>().runtimeAnimatorController = cacalienSkinOverride;
                vfxController.SetVFXForCacalien();
                toilet.sprite = cacalienToilet;
                break;
            case SkinID.Cacanaut:
                GetComponent<Animator>().runtimeAnimatorController = cacanautSkinOverride;
                vfxController.SetVFXForCacanaut();
                toilet.sprite = cacanautToilet;
                break;
            case SkinID.Cacaruto:
                GetComponent<Animator>().runtimeAnimatorController = cacarutoSkinOverride;
                vfxController.SetVFXForCacaruto();
                toilet.sprite = cacarutoToilet;
                break;
            case SkinID.Golden:
                GetComponent<Animator>().runtimeAnimatorController = goldenSkinOverride;
                vfxController.SetVFXForGolden();
                toilet.sprite = goldenToilet;
                break;
            case SkinID.Ca20s:
                GetComponent<Animator>().runtimeAnimatorController = c20sOverride;
                vfxController.SetVFXForGolden();

                Instantiate(c20sVolume, parentForInstantiatedObjects);

                break;
            default:
                Debug.LogError("No se inicializo la skin o no se encontro?");
                break;
        }
    }
}
