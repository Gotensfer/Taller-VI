using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonPickUp : MonoBehaviour
{
    [SerializeField] float powerUpTime;
    [SerializeField] float speed;
    [SerializeField] float sensibility;
    [SerializeField] GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player"); // Hay mejores formas pa esto
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PowerUp_PidgeonTest powerUp = player.AddComponent<PowerUp_PidgeonTest>();
            powerUp.duration = powerUpTime;
            powerUp.sensibility = sensibility;
            powerUp.speed = speed;
        }
    }
}
