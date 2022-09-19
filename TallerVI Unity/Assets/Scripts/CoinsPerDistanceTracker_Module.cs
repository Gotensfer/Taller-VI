using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPerDistanceTracker_Module : MonoBehaviour
{
    [SerializeField] CoinPerDistanceData coinPerDistanceData;
    [SerializeField] DistanceTracker_Module distanceTracker_Module;

    int coinsEarnedThisRun;

    float previousDistance;
    float currentDistance;
    float travelledDistance;

    /* // Debug para monedas
    private void Update()
    {
        
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
    */

    private void FixedUpdate()
    {
        currentDistance = distanceTracker_Module.travelledDistance;

        // No deber�an ocurrir casos en los que esta operaci�n de negativo, pero el sistema est� hecho
        // para corregirlo por si llegara a ocurrir.
        travelledDistance += Mathf.Max(currentDistance - previousDistance, 0); 

        if (travelledDistance > coinPerDistanceData.distanceNeededForCoinAward)
        {
            coinsEarnedThisRun += coinPerDistanceData.awardedCoinsPer_N_Distance;
            travelledDistance = 0;
        }

        previousDistance = distanceTracker_Module.travelledDistance;
    }

    // Se debe a�adir al evento Crash de PlayerEvents_Interface
    public void AwardEarnedCoinsInRun()
    {
        EconomyData.AddCoins(coinsEarnedThisRun);
    }
}