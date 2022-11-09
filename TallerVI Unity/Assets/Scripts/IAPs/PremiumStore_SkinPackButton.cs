using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremiumStore_SkinPackButton : MonoBehaviour
{
    [SerializeField] Button buyButton;
    [SerializeField] GameObject checkAsBought;

    // Esto es una terrible idea, pero solo es un boton, no??
    void FixedUpdate()
    {
        if (PremiumData.hasSkinPack)
        {
            buyButton.interactable = false;
            checkAsBought.SetActive(true);
        }
    }
}
