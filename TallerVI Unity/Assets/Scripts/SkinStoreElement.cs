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

    private void Start()
    {
        button = GetComponent<Button>();
        lockButton = lockObject.GetComponent<Button>();

        if (PlayerPrefs.GetInt($"{Enum.GetName(typeof(SkinID), (int)skin.skinID)}") == 1) // Est� desbloqueada
        {
            button.onClick.AddListener(SelectSkin);     
            lockObject.SetActive(false);
        }
        else // No est� desbloqueada
        {           
            lockObject.SetActive(true);
            lockButton.onClick.AddListener(BuySkin);
        }

        if (PlayerPrefs.GetInt($"LastSelectedSkin") == (int)skin.skinID)
        {
            SelectSkin();
        }
    }

    void SelectSkin()
    {
        SkinData.ID = skin.skinID;
        PlayerPrefs.SetInt($"LastSelectedSkin", (int)skin.skinID);
        PlayerPrefs.Save();
    }

    void BuySkin()
    {
        if (EconomyData.coins >= skin.price)
        {
            EconomyData.SpendCoins(skin.price);

            lockButton.onClick.RemoveListener(BuySkin);
            lockObject.SetActive(false);
            
            button.onClick.AddListener(SelectSkin);

            PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), (int)skin.skinID)}", 0);
            PlayerPrefs.Save();
        }
    }
}