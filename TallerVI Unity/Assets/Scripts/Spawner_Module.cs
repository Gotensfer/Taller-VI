using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner_Module : MonoBehaviour
{
    private List<List<GameObject>> pickUps_list = new List<List<GameObject>>(3);
    private List<GameObject> pickUps_listInUse =  new List<GameObject>();
    [SerializeField] private GameObject prefab1, prefab2, prefab3, father;

    //variables utilidad
    private float prevPos = 18, chance1 = 100, chance2 = 100, chance3 = 100;
    int distance, distance2, temp = 0, temp2 = 0, selected;
    private Rigidbody2D rb;
    private TrackerBase_Module track;
    private bool enabledReUse = false;

    // Start is called before the first frame update
    void Start()
    {
        track = FindObjectOfType<TrackerBase_Module>();
        rb = track.GetPlayer().GetComponent<Rigidbody2D>();
        Spawn();
    }

    void Update()
    {
        distance = (int)rb.position.y;
        distance2 = (int)rb.position.x;
        Debugger(1, pickUps_list[0].Count);
        Debugger(2, pickUps_list[1].Count);
        Debugger(3, pickUps_list[2].Count);
        Debugger(4, pickUps_listInUse.Count);
        DecreaseChance();
        ReUse();
    }

    void Debugger(int index, int count)
    {
        //Debug.Log("count" + index + ": " + count);
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

        for (int i = 0; i < 10; i++)
        {
            AddItem(0,50);
        }
    }

    public void DisablePowerUps()
    {
        enabledReUse = false;
        
        foreach (GameObject element in pickUps_listInUse)
        {
            pickUps_list[GetListIndex(element)].Add(element);

            element.transform.position = Vector3.zero;
        }
        
        pickUps_listInUse.Clear();
    }
    
    
    public void EnablePowerUps()
    {
        enabledReUse = true;
    }

    GameObject GetRandomObject()
    {
        selected = Random.Range(0, 2);

        if (selected == 0)
        {
            float rnd = Random.Range(0, 100);

            if (rnd < chance1 || rnd == 100)
            {
                if (pickUps_list[selected].Count > 0)
                {
                    return pickUps_list[selected].ElementAt(0);   
                }
                else
                {
                    return GetRandomObject();
                }
            }
        }
        else if (selected == 1)
        {
            float rnd = Random.Range(0, 100);

            if (rnd < chance2 || rnd == 100)
            {
                if (pickUps_list[selected].Count > 0)
                {
                    return pickUps_list[selected].ElementAt(0);   
                }
                else
                {
                    return GetRandomObject();
                }
            }
        }
        else if (selected == 2)
        {
            float rnd = Random.Range(0, 100);

            if (rnd < chance3 || rnd == 100)
            {
                if (pickUps_list[selected].Count > 0)
                {
                    return pickUps_list[selected].ElementAt(0);   
                }
                else
                {
                    return GetRandomObject();
                }
            }
        }

        return GetRandomObject();
    }

    float GetPrevPos()
    {
        prevPos += 10;
        return prevPos - 10;
    }

    void ReUse()
    {
        if (enabledReUse)
        {
            if (distance2 == temp2)
            {
                return;
            }
            
            temp2 = distance2;
            
            if (prevPos - 50 < distance2)
            {
                // Debug.Log("Called");
                
                if(pickUps_listInUse.Count>0) RemoveItem();
                
                AddItem(rb.position.y + Random.Range(-8.0f,0), rb.position.y + Random.Range(0,8.0f));
            }
        }
    }

    void RemoveItem()
    {
        pickUps_list[GetListIndex(pickUps_listInUse.ElementAt(0))].Add(pickUps_listInUse.ElementAt(0));
        pickUps_listInUse.RemoveAt(0);
    }

    void AddItem(float x, float y)
    {
        GameObject obj = GetRandomObject();
        obj.transform.position = new Vector3(GetPrevPos() + Random.Range(-8.0f,8.0f), Random.Range(x, y), 0);
        pickUps_list[selected].Remove(obj);
        pickUps_listInUse.Add(obj);
    }
    
    int GetListIndex(GameObject element)
    {
        if (element.GetComponent<PowerUp_Base>() != null)
        {
            if (element.GetComponent<PowerUp_Base>().GetType() == PowerUpType.chili)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }
        else
        {
            return 1;
        }
    }
    
    void DecreaseChance()
    {
        if (distance == temp)
        {
            return;
        }
        
        temp = distance;
        
        if (distance % 50 == 0)
        {
            if (distance > temp) chance1--;
            else if (distance < temp) chance1++;
        }
        else if (distance % 30 == 0)
        {
            if (distance > temp)
            {
                chance3 -= 3f;
                chance2 -= 2f;
            }
            else if (distance < temp)
            {
                chance3 += 3f;
                chance2 += 2f;
            }
            
            chance1 = FixChance(chance1);
            chance2 = FixChance(chance2);
            chance3 = FixChance(chance3);
        }
    }

    float FixChance(float chance)
    {
        if (chance > 100)
        {
            return 100;
        }
        else if (chance < 0)
        {
            return 0;
        }
        else
        {
            return chance;
        }
    }
}
