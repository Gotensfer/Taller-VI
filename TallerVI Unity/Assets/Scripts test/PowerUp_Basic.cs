using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp_Basic : MonoBehaviour
{
    [SerializeField] float strenght;
    [SerializeField] Vector2 direction;

    [SerializeField] float uses = 3;
    [SerializeField] Rigidbody2D player;

    [SerializeField] GameObject button;

    [SerializeField] PlayerEvents_Interface playerEvents;

    public void Activate_BasicPowerUp()
    {
        direction.Normalize();
        player.velocity = new Vector2(player.velocity.x, 0);
        player.AddForce(direction * strenght, ForceMode2D.Impulse);

        uses--;

        playerEvents.MitosisEvent.Invoke();

        if (uses <= 0)
        {
            button.SetActive(false);
        }
    }
}
