using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplayMoneyText : MonoBehaviour
{
    TextMeshProUGUI moneyDisplay;

    private void Start()
    {
        moneyDisplay = GetComponent<TextMeshProUGUI>();
    }

    // HORRIBLEMENTE INOPTIMO
    // CAMBIAR A ARQUITECTURA REACTIVA / POR EVENTOS
    private void Update()
    {
        moneyDisplay.text = $"{EconomyData.coins}";
    }
}
