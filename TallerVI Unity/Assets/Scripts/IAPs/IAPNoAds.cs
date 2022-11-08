using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPNoAds : MonoBehaviour
{
    [SerializeField] SkinStoreElement goldenSkin;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => print("Touched?"));
    }

    public void UnlockNoAds()
    {
        PremiumData.hasNoAds = true;
        PlayerPrefs.SetInt("HasNoAdsUnlocked", 1);

        PlayerPrefs.Save();

        print("Sanity check no ads");
    }

    public void FailPurchase()
    {
        goldenSkin.FailPurchaseSound();
    }
}
