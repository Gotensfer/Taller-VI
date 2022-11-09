using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IAPSkinPack : MonoBehaviour
{
    [SerializeField] Skin kkawaiSkin;
    [SerializeField] Skin shitsycalSkin;
    [SerializeField] Skin cacowboySkin;
    [SerializeField] Skin cacalienSkin;
    [SerializeField] Skin cacanautSkin;
    [SerializeField] Skin cacarutoSkin;
    [SerializeField] Skin c20sSkin;
    FMOD.Studio.EventInstance sfx;

    public void UnlockSkinPack()
    {
        PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)kkawaiSkin.skinID)}", 1);
        PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)shitsycalSkin.skinID)}", 1);
        PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)cacowboySkin.skinID)}", 1);
        PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)cacalienSkin.skinID)}", 1);
        PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)cacanautSkin.skinID)}", 1);
        PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)cacarutoSkin.skinID)}", 1);
        PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)c20sSkin.skinID)}", 1);

        PremiumData.hasSkinPack = true;
        PlayerPrefs.SetInt("HasSkinPackUnlocked", 1);

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
