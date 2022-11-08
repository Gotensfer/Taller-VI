using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IAPGolden : MonoBehaviour
{
    [SerializeField] SkinStoreElement goldenSkin;

    [SerializeField] GameObject storeGameObject; // Esto no es una buena implementación...
    [SerializeField] GameObject scrollviewGameObject; // Esta es una implementación aún peor...

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => print("Touched?"));
    }

    public void UnlockGolden()
    {
        storeGameObject.SetActive(true);
        scrollviewGameObject.SetActive(true);

        StartCoroutine(UnlockGoldenSkin());

        print("Sanity check golden");
    }

    public void FailPurchase()
    {
        goldenSkin.FailPurchaseSound();
    }

    IEnumerator UnlockGoldenSkin()
    {
        yield return new WaitForSeconds(0.5f);
        goldenSkin.UnlockByPayingCash();

        PremiumData.hasMidas = true;
        PlayerPrefs.SetInt("HasMidasUnlocked", 1);

        PlayerPrefs.Save();

        storeGameObject.SetActive(false);
        scrollviewGameObject.SetActive(false);
    }
}
