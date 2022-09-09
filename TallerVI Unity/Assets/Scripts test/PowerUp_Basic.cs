using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp_Basic : MonoBehaviour
{
    [SerializeField] float strenght;
    [SerializeField] Vector2 direction;

    [SerializeField] float cdTime;
    float remainingCD;

    [SerializeField] Rigidbody2D player;

    [SerializeField] GameObject button;

    [SerializeField] PlayerEvents_Interface playerEvents;

    private void Update()
    {
        if (remainingCD > 0)
        {
            remainingCD -= Time.deltaTime;
            if (remainingCD <= 0)
            {
                button.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Activate_BasicPowerUp()
    {
        direction.Normalize();
        player.velocity = new Vector2(player.velocity.x, 0);
        player.AddForce(direction * strenght, ForceMode2D.Impulse);

        remainingCD = cdTime;
        button.GetComponent<Button>().interactable = false;

        playerEvents.MitosisEvent.Invoke();
    }
}
