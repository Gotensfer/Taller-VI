using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremiumStore_NoAdsButton : MonoBehaviour
{
    [SerializeField] Button buyButton;
    [SerializeField] GameObject checkAsBought;

    // Esto es una terrible idea, pero solo es un boton, no??
    void FixedUpdate()
    {
        if (PremiumData.hasNoAds)
        {
            buyButton.interactable = false;
            checkAsBought.SetActive(true);
        }
    }
}
