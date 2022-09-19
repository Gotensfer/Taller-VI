using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeElement", menuName = "Upgrade Element")]
public class UpgradeElement : ScriptableObject
{
    public UpgradeType upgradeType;
    public int L1_CoinCost;
    public int L2_CoinCost;
    public int L3_CoinCost;
}
