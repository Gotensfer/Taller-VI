using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            col.GetComponent<PowerUpPlayer>().Powered();
            Destroy(gameObject);
        }
    }
}
