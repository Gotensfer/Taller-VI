using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLoad_DataInitializer : MonoBehaviour
{
    void Start()
    {
        // Configuración inicial de datos de economia
        if (PlayerPrefs.GetInt($"Coins", -1) == -1 )
        {
            PlayerPrefs.SetInt($"Coins", 0);
        }
        else
        {
            EconomyData.LoadCoins();
        }

        // Configuración inicial de datos de comidas
        for (int i = 0; i < Enum.GetValues(typeof(FoodID)).Length; i++)
        {
            if (PlayerPrefs.GetInt($"{Enum.GetName(typeof(FoodID), i)}", -1) == -1)
            {
                PlayerPrefs.SetInt($"{Enum.GetName(typeof(FoodID), i)}", 0);
            }            
        }

        // Configuración inicial de datos de mejoras

        // ---
        PlayerPrefs.Save();
    }
}
