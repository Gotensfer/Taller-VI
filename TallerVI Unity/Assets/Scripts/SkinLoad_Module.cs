using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoad_Module : MonoBehaviour
{
    // Esto deber�a hacerse de forma autom�tica por medio de Resources.Load o algo similar+
    // esta implementaci�n no es muy apropiada para quien implemente la UI
    // pero como Julian es tan grosero que la mame

    [SerializeField] AnimatorOverrideController kkawaiSkinOverride;
    [SerializeField] AnimatorOverrideController cacsycalSkinOverride;
    [SerializeField] AnimatorOverrideController cacowboySkinOverride;
    [SerializeField] AnimatorOverrideController cacalienSkinOverride;
    [SerializeField] AnimatorOverrideController cacanautSkinOverride;


    private void Start()
    {
        switch (SkinData.ID)
        {
            case SkinID.Base:
                break;
            case SkinID.Kkawai:
                GetComponent<Animator>().runtimeAnimatorController = kkawaiSkinOverride;
                break;
            case SkinID.Cacsycal:
                GetComponent<Animator>().runtimeAnimatorController = cacsycalSkinOverride;
                break;
            case SkinID.Cacowboy:
                GetComponent<Animator>().runtimeAnimatorController = cacowboySkinOverride;
                break;
            case SkinID.Cacalien:
                GetComponent<Animator>().runtimeAnimatorController = cacalienSkinOverride;
                break;
            case SkinID.Cacanaut:
                GetComponent<Animator>().runtimeAnimatorController = cacanautSkinOverride;
                break;
            default:
                Debug.LogError("No se inicializo la skin o no se encontro?");
                break;
        }
    }
}
