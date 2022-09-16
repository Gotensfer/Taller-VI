using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MitosisChargePickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.GetComponent<EventReferenceHandler>().playerEvents.MitosisPickUpEvent.Invoke();
            gameObject.SetActive(false);
        }
    }
}