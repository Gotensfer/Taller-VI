using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Module : MonoBehaviour
{
    [SerializeField] List<List<GameObject>> pickUps_list =  new List<List<GameObject>>();
    [SerializeField] List<Vector2> heights = new List<Vector2>();
    [SerializeField] GameObject prefab1, prefab2, prefab3, father;

    //variables utilidad
    float prevPos = 12;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Instance()
    {
        for(int i = 0; i<3; i++)
        {
            pickUps_list.Add(new List<GameObject>());
            
            for(int j = 0; j<3; j++)
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
                prevPos = 12;

                foreach (GameObject objElement in pickUps_list[count])
                {
                    objElement.transform.position = new Vector3(prevPos + Random.Range(-4.0f,4.0f), Random.Range(vectorElement.x, vectorElement.y), 0);

                    prevPos += 10;
                }
            }

            count++;
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
}
