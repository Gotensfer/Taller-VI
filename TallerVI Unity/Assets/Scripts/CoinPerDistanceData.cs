using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinPerDistance Config", menuName = "CoinPerDistance Config")]
public class CoinPerDistanceData : ScriptableObject
{
    public int awardedCoinsPer_N_Distance;
    public float distanceNeededForCoinAward; 
}
