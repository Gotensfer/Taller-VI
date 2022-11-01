using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum UpgradeType
{
    Mitosis,
    Coprolito,
    Fecalito,
    Pidgeon,
    Chilli,
    Rocket
}

public class UpgradeContainerData : MonoBehaviour
{
    [SerializeField] UpgradeType upgradeType;
    [SerializeField] int upgradeLevel = -1;

    [SerializeField] UpgradeElement upgradeElement;

    [SerializeField] TextMeshProUGUI costDisplay;

    FMOD.Studio.EventInstance sfx;
    private void Start()
    {        
        switch (upgradeType)
        {
            case UpgradeType.Mitosis:
                upgradeLevel = PlayerPrefs.GetInt("Mitosis Level", upgradeLevel);
                break;
            case UpgradeType.Fecalito:
                upgradeLevel = PlayerPrefs.GetInt("Fecalito Level", upgradeLevel);
                break;
            case UpgradeType.Pidgeon:
                upgradeLevel = PlayerPrefs.GetInt("Pidgeon Level", upgradeLevel);
                break;
            case UpgradeType.Chilli:
                upgradeLevel = PlayerPrefs.GetInt("Chilli Level", upgradeLevel);
                break;
            case UpgradeType.Rocket:
                upgradeLevel = PlayerPrefs.GetInt("Rocket Level", upgradeLevel);
                break;
        }

        switch (upgradeLevel)
        {
            case 0:
                SetZeroStar();
                break;
            case 1:
                SetOneStar();
                break;
            case 2:
                SetTwoStar();
                break;
            case 3:
                SetThreeStar();
                break;
        }
    }

    public void LevelUpUpgrade()
    {
        int cost = 0;

        switch (upgradeLevel)
        {
            case 0:
                cost = upgradeElement.L1_CoinCost;
                break;
            case 1:
                cost = upgradeElement.L2_CoinCost;
                break;
            case 2:
                cost = upgradeElement.L3_CoinCost;
                break;
            default:
                cost = 999999;
                break;
        }

        if (EconomyData.coins >= cost)
        {
            EconomyData.SpendCoins(cost);
            upgradeLevel++;
            
            switch (upgradeType)
            {
                case UpgradeType.Mitosis:
                    PlayerPrefs.SetInt("Mitosis Level", upgradeLevel);
                    break;
                case UpgradeType.Fecalito:
                    PlayerPrefs.SetInt("Fecalito Level", upgradeLevel);
                    break;
                case UpgradeType.Pidgeon:
                    PlayerPrefs.SetInt("Pidgeon Level", upgradeLevel);
                    break;
                case UpgradeType.Chilli:
                    PlayerPrefs.SetInt("Chilli Level", upgradeLevel);
                    break;
                case UpgradeType.Rocket:
                    PlayerPrefs.SetInt("Rocket Level", upgradeLevel);
                    break;
            }
            //sfx money
            sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/Buy");
            sfx.start();
            PlayerPrefs.Save();
        }
        else
        {
            //sfx no money
            sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/No money"); //sfx no money
            sfx.start();
        }

        switch (upgradeLevel)
        {
            case 0:
                SetZeroStar();
                break;
            case 1:
                SetOneStar();
                break;
            case 2:
                SetTwoStar();
                break;
            case 3:
                SetThreeStar();
                break;
        }
    }

    [SerializeField] Image FirstStar;
    [SerializeField] Image SecondStar;
    [SerializeField] Image ThirdStar;

    [SerializeField] Sprite BlackStar;
    [SerializeField] Sprite GoldenStar;

    public void SetZeroStar()
    {
        FirstStar.sprite = BlackStar;
        SecondStar.sprite = BlackStar;
        ThirdStar.sprite = BlackStar;

        if (upgradeElement.L1_CoinCost == 0) costDisplay.text = "";
        costDisplay.text = $"${upgradeElement.L1_CoinCost}";
    }

    public void SetOneStar()
    {
        FirstStar.sprite = GoldenStar;
        SecondStar.sprite = BlackStar; 
        ThirdStar.sprite = BlackStar;

        costDisplay.text = $"${upgradeElement.L2_CoinCost}";
    }

    public void SetTwoStar()
    {
        FirstStar.sprite = GoldenStar;
        SecondStar.sprite = GoldenStar;
        ThirdStar.sprite = BlackStar;

        costDisplay.text = $"${upgradeElement.L3_CoinCost}";
    }

    public void SetThreeStar()
    {
        FirstStar.sprite = GoldenStar;
        SecondStar.sprite = GoldenStar;
        ThirdStar.sprite = GoldenStar;

        costDisplay.text = "";
    }

    private void OnDisable()
    {
        sfx.release();
    }
}
