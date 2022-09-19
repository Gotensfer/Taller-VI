using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {        
        if (upgradeType == UpgradeType.Mitosis)
        {
            upgradeLevel = PlayerPrefs.GetInt("Mitosis Level", -1);
        }
        else if (upgradeType == UpgradeType.Fecalito)
        {
            upgradeLevel = PlayerPrefs.GetInt("Fecalito Level", -1);
        }

        switch(upgradeLevel)
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

            if (upgradeType == UpgradeType.Mitosis)
            {
                PlayerPrefs.SetInt("Mitosis Level", upgradeLevel);
            }
            else if (upgradeType == UpgradeType.Fecalito)
            {
                PlayerPrefs.SetInt("Fecalito Level", upgradeLevel);
            }

            PlayerPrefs.Save();
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
    }

    public void SetOneStar()
    {
        FirstStar.sprite = GoldenStar;
        SecondStar.sprite = BlackStar; 
        ThirdStar.sprite = BlackStar;
    }

    public void SetTwoStar()
    {
        FirstStar.sprite = GoldenStar;
        SecondStar.sprite = GoldenStar;
        ThirdStar.sprite = BlackStar;
    }

    public void SetThreeStar()
    {
        FirstStar.sprite = GoldenStar;
        SecondStar.sprite = GoldenStar;
        ThirdStar.sprite = GoldenStar;
    }
}
