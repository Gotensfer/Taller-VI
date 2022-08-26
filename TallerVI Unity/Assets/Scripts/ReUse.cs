using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReUse : MonoBehaviour
{
    Spawner_Module spawner;
    private TrackerBase_Module track;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        track = FindObjectOfType<TrackerBase_Module>();
        rb = track.GetPlayer().GetComponent<Rigidbody2D>();
        spawner = FindObjectOfType<Spawner_Module>();
    }

    // Update is called once per frame
    void Update()
    {
        Visible();
    }

    void Visible()
    {
        var cam = Camera.main;
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var objCollider = GetComponent<Collider2D>();

        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        {
            return;
        }
        else if (rb.transform.position.x < transform.position.x)
        {
            return;
        }

        foreach (Vector2 element in spawner.GetVectors())
        {
            if (gameObject.transform.position.y <= element.y && gameObject.transform.position.y >= element.x)
            {
                transform.position = new Vector3(spawner.GetPrevPos(), Random.Range(element.x, element.y), 0);
                break;
            }
        }

        if (GetComponent<SpriteRenderer>().enabled == false)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
