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
    }

    public static void SpendCoins(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError($"EL VALOR INGRESADO {amount} NO PUEDE SER NEGATIVO");
            return;
        }

        coins -= coins;
    }

    // Usado para cargar las monedas al inicializar el juego
    public static void SetCoins(int amount)
    {
        coins = amount;
    }

    public static void ResetCoins()
    {
        coins = 0;
    }
}
