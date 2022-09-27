using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    chili,
    rocket
}

public class PowerUp_Base : MonoBehaviour, IPowerUp
{
    [SerializeField] private PowerUpType type;
    private TrackerBase_Module track;
    private Rigidbody2D rb;
    private bool active = false, calleable = true;
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

    float count = 0; // Antes lo estabas definiendo en el mismo Use(), haciendo que nunca sumara xd - Juanfer

    void Use()
    {
        if (active)
        {
            if (count >= time)
            {
                active = false;
                CallEvent();
            }
            count += Time.deltaTime;
            
            rb.AddForce(new Vector2(Mathf.Cos(conversion), Mathf.Sin(conversion)) * (force * Time.deltaTime), ForceMode2D.Impulse);

            if (angle == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }

    public SpriteRenderer sp;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().CompareTag("Player"))
        {
            active = true;
            calleable = true;
            transform.position = Vector3.zero;

            // VITAL: Referente a los eventos
            // Invocaci�n al evento
            
            transform.parent.GetComponent<EventReferenceHandler>().playerEvents.PoweredUpEvent.Invoke();

            // OJO: Falta una forma de distinguir que PowerUp es, por defecto todos los powerUps que usen esta clase son Chilli !!
            // !! Los powerups deben invocar su evento correspondiente !!
            if (type == PowerUpType.rocket)
            {
                transform.parent.GetComponent<EventReferenceHandler>().playerEvents.RocketEvent.Invoke();
            }
            else if (type == PowerUpType.chili)
            {
                transform.parent.GetComponent<EventReferenceHandler>().playerEvents.ChilliEvent.Invoke();
            }
        }    
    }

    public PowerUpType GetType()
    {
        return type;
    }

    void CallEvent()
    {
        if (calleable)
        {
            calleable = false;
            transform.parent.GetComponent<EventReferenceHandler>().playerEvents.PoweredDownEvent.Invoke();
            gameObject.SetActive(false);

            if (type == PowerUpType.rocket)
            {
                transform.parent.GetComponent<EventReferenceHandler>().playerEvents.rocketDownEvent.Invoke();
            }
            else if (type == PowerUpType.chili)
            {
                transform.parent.GetComponent<EventReferenceHandler>().playerEvents.chilliDownEvent.Invoke();
            }
        }
    }
}
