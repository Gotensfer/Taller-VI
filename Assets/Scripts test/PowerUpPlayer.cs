using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPlayer : MonoBehaviour
{
    Vector3 touchPosWorld;

    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Ended;

    public GameObject ps;
    bool power = false;
    Vector2 direction;
    Rigidbody2D rb;

    void Start()
    {
        direction = new Vector2(0, 1);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
        {
            //We transform the touch position into word space from screen space and store it.
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;

                if(touchedObject.transform.name == "Player" && power)
                {
                    direction.Normalize();
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(direction * 10, ForceMode2D.Impulse);
                    Debug.Log("Touched ");
                }
            }
        }
    }

    public void Powered()
    {
        ps.SetActive(true);
        power = true;
        Invoke("Reset", 7f);
    }

    void Reset()
    {
        ps.SetActive(false);
        power = false;
    }
}
