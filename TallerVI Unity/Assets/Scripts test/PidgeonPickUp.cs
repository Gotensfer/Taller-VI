using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonPickUp : MonoBehaviour, IPowerUp
{
    [SerializeField] float powerUpTime;
    [SerializeField] float speed;
    [SerializeField] float sensibility;
    [SerializeField] GameObject player;


    Rigidbody2D rb;

    private void Awake()
    {
        player = GameObject.Find("Player"); // Hay mejores formas pa esto
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.GetComponent<PowerUp_PidgeonTest>() != null)
            {
                collision.GetComponent<PowerUp_PidgeonTest>().InterruptAndStop(); // TODO: Figurar como estandarizar con los demas PowerUps
            }

            PowerUp_PidgeonTest powerUp = player.AddComponent<PowerUp_PidgeonTest>();
            powerUp.duration = powerUpTime;
            powerUp.sensibility = sensibility;
            powerUp.speed = speed;
            powerUp.Initialize();

            if (player.GetComponent<FlyingStates_Module>().PlayerState != PlayerStates.poweredUpFlying)
            {
                transform.parent.GetComponent<EventReferenceHandler>().playerEvents.PoweredUpEvent.Invoke();
            }

            transform.parent.GetComponent<EventReferenceHandler>().playerEvents.PidgeonEvent.Invoke();

            #region"VFX"

            // En caso de necesitar implementarlos aca (?

            #endregion

            gameObject.SetActive(false); // Esto desactivara a la paloma junto con todos sus hijos apenas se coja, ojo

            /* Codigo deprecado antes de la implementación de la "generacion" aleatoria (Pooling) 
            Destroy(gameObject); // Esto destruira a la paloma juntos con sus hijos apenas se coja, ojo
            */


        }      
    }
}
