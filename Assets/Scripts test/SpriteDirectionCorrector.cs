using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDirectionCorrector : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform player;
    [SerializeField] Vector3 orientation;

    private void FixedUpdate()
    {
        orientation = (rb.velocity + (Vector2)player.position).normalized;
        player.LookAt(orientation, Vector3.left);
        player.Rotate(0, -90, 0);
    }
}
