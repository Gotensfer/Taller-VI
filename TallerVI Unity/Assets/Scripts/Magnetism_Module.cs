using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism_Module : MonoBehaviour
{
    Transform magnetizedPickUp;
    int attractingPickUps;
    Vector3 offsetPositionPickUp;
    Vector3 movementVector;
    Vector3 moveDirection;
    [SerializeField] float speed;
    [SerializeField] float relativeDisplacement;
    float initialTime;

    private void FixedUpdate()
    {
        if (magnetizedPickUp == null) return;

        relativeDisplacement = Time.time - initialTime;
        magnetizedPickUp.position = transform.position - offsetPositionPickUp + (moveDirection * relativeDisplacement * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print($"Entered collision with {collision.name}");

        if (collision.GetComponent<IPowerUp>() != null)
        {     
            //print($"Collided with the PickUp {collision.name}");
            magnetizedPickUp = collision.transform;
            offsetPositionPickUp = transform.position - magnetizedPickUp.position;
   
            moveDirection = offsetPositionPickUp.normalized;

            initialTime = Time.time;

            attractingPickUps++; // Hotfix a las 2 am : TODO -> Dos poderes podran ser atraidos al tiempo? Que sucedera?
        }        
    }

    // IMPORTANTE: Por ahora SOLO el último pickup que se toco con el area magnética sera atraido.
    // TODO: Meter cambio de radio dinámico, por ahora solo es por el inspector (Lo cual podría ser incomodo de cuadrar con otros sistemas luego)

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print($"Left collision with {collision.name}");
        if (collision.GetComponent<IPowerUp>() != null)
        {
            //print($"Left the PickUp {collision.name}");
            if (attractingPickUps == 1)
            {
                magnetizedPickUp = null;
            }
            
            attractingPickUps--; // Hotfix a las 2 am : TODO -> Dos poderes podran ser atraidos al tiempo? Que sucedera?
        }
    }
}
