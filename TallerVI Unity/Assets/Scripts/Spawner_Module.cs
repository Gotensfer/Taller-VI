using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner_Module : MonoBehaviour
{
    [SerializeField] List<List<GameObject>> pickUps_list =  new List<List<GameObject>>();
    [SerializeField] List<Vector2> heights = new List<Vector2>();
    [SerializeField] GameObject prefab1, prefab2, prefab3, father;

    //variables utilidad
    float prevPos = 18;
    private Rigidbody2D rb;
    private TrackerBase_Module track;
    Camera cam;
    private Plane[] planes;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        track = FindObjectOfType<TrackerBase_Module>();
        rb = track.GetPlayer().GetComponent<Rigidbody2D>();
        Spawn();
    }

    void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        ReUse();
    }

    void Instance()
    {
        for(int i = 0; i<3; i++)
        {
            pickUps_list.Add(new List<GameObject>());
            
            for(int j = 0; j<20; j++)
            {
                if(i==0)
                {
                    pickUps_list[i].Add(Instantiate(prefab1,father.transform));
                }
                else if(i==1)
                {
                    pickUps_list[i].Add(Instantiate(prefab2,father.transform));
                }
                else
                {
                    pickUps_list[i].Add(Instantiate(prefab3,father.transform));
                }
            }
        }
    }

    void Spawn()
    {
        Instance();

        byte count = 0;

        foreach(Vector2 vectorElement in heights)
        {
            if(pickUps_list[count] != null)
            {
                prevPos = 18;

                foreach (GameObject objElement in pickUps_list[count])
                {
                    objElement.transform.position = new Vector3(prevPos + Random.Range(-4.0f,4.0f), Random.Range(vectorElement.x, vectorElement.y), 0);

                    prevPos += 10;
                }
            }

            count++;
        }
    }

    public void DisablePowerUps()
    {
        foreach (List<GameObject> element in pickUps_list)
        {
            foreach (GameObject obj in element)
            {
                obj.GetComponent<Collider2D>().enabled = false;
                if (obj.GetComponent<PowerUp_Base>() != null)
                {
                    obj.GetComponent<PowerUp_Base>().sp.enabled = false;
                }
                else
                {
                    obj.GetComponentInChildren<ParticleSystem>().Stop();
                }
            }
        }
    }
    
    public void EnablePowerUps()
    {
        foreach (List<GameObject> element in pickUps_list)
        {
            foreach (GameObject obj in element)
            {
                obj.GetComponent<Collider2D>().enabled = true;
                if (obj.GetComponent<PowerUp_Base>() != null)
                {
                    obj.GetComponent<PowerUp_Base>().sp.enabled = true;
                }
                obj.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }

    public List<Vector2> GetVectors()
    {
        return heights;
    }

    public float GetPrevPos()
    {
        prevPos += 10;
        return prevPos - 10;
    }

    void ReUse()
    {
        foreach (List<GameObject> element in pickUps_list)
        {
            foreach (GameObject obj in element)
            {
                Visible(obj.GetComponent<Collider2D>());
            }
        }
    }
    
    void Visible(Collider2D col)
    {
        if (GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            return;
        }
        else if (rb.transform.position.x < col.transform.position.x+10)
        {
            return;
        }

        foreach (Vector2 element in GetVectors())
        {
            if (col.transform.position.y <= element.y && col.transform.position.y >= element.x)
            {
                col.transform.position = new Vector3(GetPrevPos(), Random.Range(element.x, element.y), 0);
                break;
            }
        }
    }
}
