using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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

                // HARDCODEADO, MALO MALO MALOOOO
                if (i == (int)FoodID.Guaro)
                {
                    PlayerPrefs.SetInt($"{Enum.GetName(typeof(FoodID), i)}", 1);
                }
                if (i == (int)FoodID.BandejaPaisa)
                {
                    PlayerPrefs.SetInt($"{Enum.GetName(typeof(FoodID), i)}", 1);
                }
                if (i == (int)FoodID.Brevas)
                {
                    PlayerPrefs.SetInt($"{Enum.GetName(typeof(FoodID), i)}", 1);
                }
            }            
        }

        // Configuración inicial de datos de mejoras

        // NOTA: EL SISTEMA SE CAMBIARA A UNO SIMILAR USADO PARA LAS COMIDAS
        if (PlayerPrefs.GetInt($"Mitosis Level", -1) == -1)
        {
            PlayerPrefs.SetInt($"Mitosis Level", 1);
        }

        if (PlayerPrefs.GetInt($"Fecalito Level", -1) == -1)
        {
            PlayerPrefs.SetInt($"Fecalito Level", 0);
        }

        // ---
        PlayerPrefs.Save();

        SceneManager.LoadScene(1);
    }
}
