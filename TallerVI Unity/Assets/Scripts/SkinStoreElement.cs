using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class SkinStoreElement : MonoBehaviour
{
    [SerializeField] Skin skin;
    [SerializeField] GameObject lockObject;
    Button button;
    Button lockButton;
    
    FMOD.Studio.EventInstance sfx;

    [SerializeField] bool premiumSkin = false;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        lockButton = lockObject.GetComponent<Button>();

        print(PlayerPrefs.GetInt($"{Enum.GetName(typeof(SkinID), (int)skin.skinID)}"));
        if (PlayerPrefs.GetInt($"{Enum.GetName(typeof(SkinID), (int)skin.skinID)}") == 1) // Est� desbloqueada
        {
            
            button.onClick.AddListener(SelectSkin);     
            lockObject.SetActive(false);
        }
        else // No est� desbloqueada
        {           
            lockObject.SetActive(true);
            lockButton.onClick.AddListener(BuySkin);

            // Si es una skin premium, el comportamiento lo controlar� el IAP Button
            if (premiumSkin) lockButton.onClick.RemoveListener(BuySkin);
        }

        if (PlayerPrefs.GetInt($"LastSelectedSkin") == (int)skin.skinID)
        {
            button.onClick.Invoke();
        }
    }

    void SelectSkin()
    {
        SkinData.ID = skin.skinID;
        PlayerPrefs.SetInt($"LastSelectedSkin", (int)skin.skinID);
        PlayerPrefs.Save();
    }

    public void BuySkin()
    {
        if (EconomyData.coins >= skin.price)
        {
            EconomyData.SpendCoins(skin.price);

            lockButton.onClick.RemoveListener(BuySkin);
            lockObject.SetActive(false);
            
            button.onClick.AddListener(SelectSkin);
            //sfx candado
            sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/Unlock");
            sfx.start();
            print("a");
            PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)skin.skinID)}", 1);
            PlayerPrefs.Save();
        }
        else
        {
            //sfx no money
            sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/No money");
            sfx.start();
        }
    }

    public void UnlockByPayingCash()
    {
        lockButton.onClick.RemoveListener(BuySkin);
        lockObject.SetActive(false);

        button.onClick.AddListener(SelectSkin);
        //sfx candado
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/Unlock");
        sfx.start();
        print("a");
        PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)skin.skinID)}", 1);
        PlayerPrefs.Save();
    }

    public void FailPurchaseSound()
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/No money");
        sfx.start();
    }

    private void OnDisable()
    {
        sfx.release();
    }
}