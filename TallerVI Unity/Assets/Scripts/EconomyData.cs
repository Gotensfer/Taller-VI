using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EconomyData
{
    public static int coins = 0;

    public static void AddCoins(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError($"EL VALOR INGRESADO {amount} NO PUEDE SER NEGATIVO");
            return;
        }

        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    public static void SpendCoins(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError($"EL VALOR INGRESADO {amount} NO PUEDE SER NEGATIVO");
            return;
        }

        coins -= amount;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    // Usado para cargar las monedas al inicializar el juego
    public static void LoadCoins()
    {
        coins = PlayerPrefs.GetInt("Coins", -1); // Si llega a devolver -1 es debido a un error ya que no se cargaron las monedas

        if (coins == -1)
        {
            Debug.LogError($"PLAYER PREFS - COINS - NO SE PUDO OBTENER");
            return;
        }

        PlayerPrefs.Save();
    }

    public static void SetCoins(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError($"EL VALOR INGRESADO {amount} NO PUEDE SER NEGATIVO");
            return;
        }

        coins = amount;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    public static void ResetCoins()
    {
        coins = 0;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }
}
