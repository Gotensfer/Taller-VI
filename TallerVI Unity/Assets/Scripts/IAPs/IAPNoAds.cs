using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPNoAds : MonoBehaviour
{
    FMOD.Studio.EventInstance sfx;

    public void UnlockNoAds()
    {
        PremiumData.hasNoAds = true;
        PlayerPrefs.SetInt("HasNoAdsUnlocked", 1);

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
