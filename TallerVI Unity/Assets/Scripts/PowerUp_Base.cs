using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Base : MonoBehaviour
{
    private TrackerBase_Module track;
    [SerializeField] SpriteRenderer sp;
    private Rigidbody2D rb;
    private bool active = false;
    [SerializeField] private float time = 3.0f, force = 10f, angle = 45f;
    private float conversion;
    
    void Start()
    {
        track = FindObjectOfType<TrackerBase_Module>();
        rb = track.GetPlayer().GetComponent<Rigidbody2D>();
        conversion = Mathf.Deg2Rad * angle;
    }

    // Update is called once per frame
    void Update()
    {
        Use();
    }

    void Use()
    {
        if (active)
        {
            float count = 0;
            count += Time.deltaTime;
            
            rb.AddForce(new Vector2(Mathf.Cos(conversion), Mathf.Sin(conversion)) * (force * Time.deltaTime), ForceMode2D.Impulse);

            if (angle == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            
            if (count >= time)
            {
                active = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            active = true;
            sp.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            #region"VFX"

            #endregion

            Destroy(gameObject, time); // Esto destruira al chilli/cohete juntos con sus hijos apenas se coja, ojo
            // Nota, este Script es el mismo tanto para Chilli como para el cohete
        }    
    }
}
