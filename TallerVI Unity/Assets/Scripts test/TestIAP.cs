using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIAP : MonoBehaviour
{
    public void SetNoAds()
    {
        print("Got no ads");
    }

    public void SetSkinPack()
    {
        print("Got skin pack");
    }

    public void SetGoldenCacaleta()
    {
        print("Got golden cacaleta");
    }

    public void FailedPurchase()
    {
        print("Failed to buy");
    }
}
