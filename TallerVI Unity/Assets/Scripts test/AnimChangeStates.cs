using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent (typeof(Animator))]
public class AnimChangeStates : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Collider2D floorCollider;
    [SerializeField] Collider2D playerCollider;
    private Animator animator;
    private int count = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (count==1 && !playerCollider.IsTouching(floorCollider))
        {
            ChangingHeight();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (count == 0)
        {
            count++;
            return;
        }
        else if (count ==1 ||count==2 && playerCollider.IsTouching(floorCollider))
        {
            animator.SetTrigger("Crash");
            count++;
            Debug.Log("Si");
        }


    }

    void ChangingHeight()
    {
        if (rb.velocity.y > 1 && count == 1)
        {
            animator.SetTrigger("FlyUp");
        }
        else if (rb.velocity.y <= 1 && rb.velocity.y >= 1 && count == 1)
        {
            animator.SetTrigger("Gliding");
        }
        else if (rb.velocity.y < -1 && count == 1)
        {
            animator.SetTrigger("FlyDown");
        }
    }

    public void PowerUp()
    {
        animator.SetTrigger("PowerUp");
    }
}
