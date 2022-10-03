using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameLoad_DataInitializer : MonoBehaviour
{
    void Start()
    {
        #region"Configuración inicial de datos de economia"
        if (PlayerPrefs.GetInt($"Coins", -1) == -1 )
        {
            PlayerPrefs.SetInt($"Coins", 0);
        }
        else
        {
            EconomyData.LoadCoins();
        }
        #endregion

        #region"Configuración inicial de datos de comidas"
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
        #endregion

        #region"Configuracion inicial de datos de mejoras

        // NOTA: EL SISTEMA SE CAMBIARA A UNO SIMILAR USADO PARA LAS COMIDAS
        if (PlayerPrefs.GetInt($"Mitosis Level", -1) == -1)
        {
            PlayerPrefs.SetInt($"Mitosis Level", 1);
        }

        if (PlayerPrefs.GetInt($"Fecalito Level", -1) == -1)
        {
            PlayerPrefs.SetInt($"Fecalito Level", 0);
        }

        if (PlayerPrefs.GetInt($"Chilli Level", -1) == -1)
        {
            PlayerPrefs.SetInt($"Chilli Level", 1);
        }

        if (PlayerPrefs.GetInt($"Pidgeon Level", -1) == -1)
        {
            PlayerPrefs.SetInt($"Pidgeon Level", 0);
        }

        if (PlayerPrefs.GetInt($"Rocket Level", -1) == -1)
        {
            PlayerPrefs.SetInt($"Rocket Level", 0);
        }

        // Configuración inicial de datos de logros

        if (PlayerPrefs.GetInt($"Achievement1", -1) == -1) // 2k
        {
            PlayerPrefs.SetInt($"Achievement1", 0);
        }

        if (PlayerPrefs.GetInt($"Achievement2", -1) == -1) // 4k
        {
            PlayerPrefs.SetInt($"Achievement2", 0);
        }

        if (PlayerPrefs.GetInt($"Achievement3", -1) == -1) // 6k
        {
            PlayerPrefs.SetInt($"Achievement3", 0);
        }

        if (PlayerPrefs.GetInt($"Achievement4", -1) == -1) // 8k
        {
            PlayerPrefs.SetInt($"Achievement4", 0);
        }

        if (PlayerPrefs.GetInt($"Achievement5", -1) == -1) // 500m alt
        {
            PlayerPrefs.SetInt($"Achievement5", 0);
        }
        #endregion

        #region"Configuración inicial de datos de tutorial
        if (PlayerPrefs.GetInt($"t1", -1) == -1) 
        {
            PlayerPrefs.SetInt($"t1", 0);
        }

        if (PlayerPrefs.GetInt($"t1", -1) == -1)
        {
            PlayerPrefs.SetInt($"t1", 0);
        }

        if (PlayerPrefs.GetInt($"t1", -1) == -1)
        {
            PlayerPrefs.SetInt($"t1", 0);
        }

        if (PlayerPrefs.GetInt($"t1", -1) == -1)
        {
            PlayerPrefs.SetInt($"t1", 0);
        }

        if (PlayerPrefs.GetInt($"t1", -1) == -1)
        {
            PlayerPrefs.SetInt($"t1", 0);
        }

        #endregion

        PlayerPrefs.Save();

        SceneManager.LoadScene(1);
    }
}
