using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperCheats : MonoBehaviour
{
    private void Update()
    {
        // Cheats para monedas
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EconomyData.AddCoins(50);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            EconomyData.SpendCoins(50);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            print(EconomyData.coins);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            EconomyData.LoadCoins();
        }
    }
}
