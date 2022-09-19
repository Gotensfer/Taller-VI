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

    private void FixedUpdate()
    {
        currentDistance = distanceTracker_Module.travelledDistance;

        // No deberían ocurrir casos en los que esta operación de negativo, pero el sistema está hecho
        // para corregirlo por si llegara a ocurrir.
        travelledDistance += Mathf.Max(currentDistance - previousDistance, 0); 

        if (travelledDistance > coinPerDistanceData.distanceNeededForCoinAward)
        {
            coinsEarnedThisRun += coinPerDistanceData.awardedCoinsPer_N_Distance;
            travelledDistance = 0;
        }

        previousDistance = distanceTracker_Module.travelledDistance;
    }

    // Se debe añadir al evento Crash de PlayerEvents_Interface
    public void AwardEarnedCoinsInRun()
    {
        EconomyData.AddCoins(coinsEarnedThisRun);
    }
}
