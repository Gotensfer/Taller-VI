using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_CheckCoins : MonoBehaviour
{
    [SerializeField] int coinsNeededForTutorial;

    // Este método debe estar agregado en CrashEvent
    public void CheckCoins()
    {
        if (EconomyData.coins >= coinsNeededForTutorial && PlayerPrefs.GetInt("firstTimeUpgrades") == 1)
        {
            // Lo que haga el tutorial va aquí

        }
    }
}