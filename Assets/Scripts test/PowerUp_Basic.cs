using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Basic : MonoBehaviour
{
    [SerializeField] float strenght;
    [SerializeField] Vector2 direction;

    [SerializeField] Rigidbody2D player;
    public void Activate_BasicPowerUp()
    {
        direction.Normalize();
        player.velocity = new Vector2(player.velocity.x, 0);
        player.AddForce(direction * strenght, ForceMode2D.Impulse);
    }
}
