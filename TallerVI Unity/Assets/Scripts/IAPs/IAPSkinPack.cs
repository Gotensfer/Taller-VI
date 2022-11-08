using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPSkinPack : MonoBehaviour
{
    [SerializeField] SkinStoreElement kkawaiSkin;
    [SerializeField] SkinStoreElement shitsycalSkin;
    [SerializeField] SkinStoreElement cacowboySkin;
    [SerializeField] SkinStoreElement cacalienSkin;
    [SerializeField] SkinStoreElement cacanautSkin;
    [SerializeField] SkinStoreElement cacarutoSkin;
    [SerializeField] SkinStoreElement c20sSkin;

    [SerializeField] GameObject storeGameObject; // Esto no es una buena implementación...
    [SerializeField] GameObject scrollviewGameObject; // Esta es una implementación aún peor...

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => print("Touched?"));
    }

    public void UnlockSkinPack()
    {
        storeGameObject.SetActive(true);
        scrollviewGameObject.SetActive(true);

        StartCoroutine(UnlockSkins());

        print("Sanity check skin pack");
    }

    public void FailPurchase()
    {
        kkawaiSkin.FailPurchaseSound();
    }

    IEnumerator UnlockSkins()
    {
        yield return new WaitForSeconds(0.5f);

        kkawaiSkin.UnlockByPayingCash();
        shitsycalSkin.UnlockByPayingCash();
        cacowboySkin.UnlockByPayingCash();
        cacalienSkin.UnlockByPayingCash();
        cacanautSkin.UnlockByPayingCash();
        cacarutoSkin.UnlockByPayingCash();
        c20sSkin.UnlockByPayingCash();

        PremiumData.hasSkinPack = true;
        PlayerPrefs.SetInt("HasSkinPackUnlocked", 1);

        PlayerPrefs.Save();

        storeGameObject.SetActive(false);
        scrollviewGameObject.SetActive(false);
    }
}
