using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IAPGolden : MonoBehaviour
{
    [SerializeField] Skin goldenSkin;
    FMOD.Studio.EventInstance sfx;

    public void UnlockGolden()
    {
        PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)goldenSkin.skinID)}", 1);

        PremiumData.hasMidas = true;
        PlayerPrefs.SetInt("HasMidasUnlocked", 1);

        PlayerPrefs.Save();

        // Sonido
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/Unlock");
        sfx.start();
    }

    public void FailPurchase()
    {
        // Sonido
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/No money");
        sfx.start();
    }

    private void OnDisable()
    {
        sfx.release();
    }
}
