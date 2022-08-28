using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnetism : MonoBehaviour
{
    bool magnetized;
    Transform target;
    Vector3 direction;
    [SerializeField] float speedModifier;
    float speed;

    private void FixedUpdate()
    {
        if (magnetized)
        {
            direction = (target.position - transform.position).normalized;
            transform.parent.Translate(speed * Time.fixedDeltaTime * direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            magnetized = true;
            target = collision.transform;
            speed = collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude * speedModifier;

            transform.parent.transform.parent = collision.transform;
        }
    }
}
