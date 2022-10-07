using UnityEngine;

public class PowerUp_Rocket : MonoBehaviour
{
    private TrackerBase_Module track;
    private Rigidbody2D rb;
    private bool active = false, calleable = true;
    [SerializeField] private float time = 3.0f, force = 10f, angle = 45f, forceLimit = 10f;
    private float conversion;
    
    Vector3 touchPosWorld;
    TouchPhase touchPhase = TouchPhase.Ended;

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
        Touch();
    }

    float count = 0;

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

            rb.AddForce(new Vector2(Mathf.Cos(conversion), Mathf.Sin(conversion)) * (force * Time.deltaTime),
                ForceMode2D.Impulse);

            if (angle == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().CompareTag("Player"))
        {
            active = true;
            calleable = true;
            transform.position = Vector3.zero;

            transform.parent.GetComponent<EventReferenceHandler>().playerEvents.PoweredUpEvent.Invoke();
            
            transform.parent.GetComponent<EventReferenceHandler>().playerEvents.RocketEvent.Invoke();
        }
    }

    void Touch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
        {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null)
            {
                GameObject touchedObject = hitInformation.transform.gameObject;

                if(touchedObject.transform.name == "Player" && active && force<forceLimit)
                {
                    force += 2f;
                    Debug.Log("Touched");
                }
            }
        }
    }
    
    void CallEvent()
    {
        if (calleable)
        {
            calleable = false;
            transform.parent.GetComponent<EventReferenceHandler>().playerEvents.PoweredDownEvent.Invoke();
            gameObject.SetActive(false);
            
            transform.parent.GetComponent<EventReferenceHandler>().playerEvents.rocketDownEvent.Invoke();
        }
    }
}
