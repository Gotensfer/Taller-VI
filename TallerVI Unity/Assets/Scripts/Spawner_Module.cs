using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner_Module : MonoBehaviour
{
    private List<List<GameObject>> pickUps_list = new List<List<GameObject>>(4);
    private List<GameObject> pickUps_listInUse =  new List<GameObject>();
    [SerializeField] private GameObject prefab1, prefab2, prefab3, prefab4, father;

    //variables utilidad
    private float prevPos = 0, chance1 = 100, chance2 = 100, chance3 = 100, chance4 = 100;
    int distance, distance2, temp = 0, temp2 = 0, selected;
    private Rigidbody2D rb;
    private TrackerBase_Module track;
    private bool enabledReUse = false, gliding = false, falling = false, spawnedLast = false, enableChange = false;

    // Start is called before the first frame update
    void Start()
    {
        track = FindObjectOfType<TrackerBase_Module>();
        rb = track.GetPlayer().GetComponent<Rigidbody2D>();
        Instance();
    }

    void Update()
    {
        distance = (int)rb.position.y;
        distance2 = (int)rb.position.x;
        
        if (gliding && enableChange)
        {
            enableChange = false;
            prevPos = rb.transform.position.x + 10;
        }
        else if(falling && enableChange)
        {
            enableChange = false;
            prevPos = rb.transform.position.y - 5;
        }

        #region Debuggers
        
        /*
        Debugger(1, pickUps_list[0].Count);
        Debugger(2, pickUps_list[1].Count);
        Debugger(3, pickUps_list[2].Count);
        Debugger(4, pickUps_list[3].Count);
        Debugger(5, pickUps_listInUse.Count);
        */
        
        #endregion
        
        DecreaseChance();
        ReUse();
    }

    void Debugger(int index, int count)
    {
        Debug.Log("count" + index + ": " + count);
    }

    public void Gliding(bool change)
    {
        gliding = change;
        enableChange = true;
    }
    
    public void Falling(bool change)
    {
        falling = change;
        enableChange = true;
    }
    
    void Instance()
    {
        for(int i = 0; i<4; i++)
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
                else if(i==2)
                {
                    pickUps_list[i].Add(Instantiate(prefab3,father.transform));
                }
                else
                {
                    pickUps_list[i].Add(Instantiate(prefab4,father.transform));
                }
            }
        }
    }

    public void DisablePowerUps()
    {
        Debug.Log("Disable");
        
        enabledReUse = false;
        
        foreach (Transform element in father.transform)
        {
            element.transform.position = Vector3.zero;
        }
        
        ClearInUse();
    }
    
    
    public void EnablePowerUps()
    {
        Debug.Log("Enable");
        Invoke("TrueState", 0.5f);
    }

    void TrueState()
    {
        enabledReUse = true;
        enableChange = true;
    }
    
    GameObject GetRandomObject()
    {
        selected = Random.Range(0, 4);

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
        else if (selected == 3)
        {
            float rnd = Random.Range(0, 100);
        
            if (rnd < chance4 || rnd == 100)
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
    
    float NegGetPrevPos()
    {
        prevPos -= 5;
        return prevPos + 5;
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

            if (prevPos - 40 < distance2 && gliding)
            {
                // Debug.Log("Called");
                
                if(pickUps_listInUse.Count>0) RemoveItem();
                
                AddItem();
            }
            else if(prevPos + 10 > distance && falling)
            {
                if(pickUps_listInUse.Count>0) RemoveItem();
                
                AddItem();
            }
        }
    }

    void RemoveItem()
    {
        pickUps_list[GetListIndex(pickUps_listInUse.ElementAt(0))].Add(pickUps_listInUse.ElementAt(0));
        pickUps_listInUse.RemoveAt(0);
    }

    void AddItem()
    {
        GameObject obj = GetRandomObject();

        if (gliding)
        {
            obj.transform.position = new Vector3(GetPrevPos() + Random.Range(-1.0f, 1.0f), rb.transform.position.y + Random.Range(-5.0f, 1.0f), 0);
        }

        if (falling)
        {
            obj.transform.position = new Vector3(Random.Range(2.0f, 4.0f) + (FuturePosition(0.5f)), NegGetPrevPos() + Random.Range(-1.0f, 1.0f), 0);
        }
        
        if (obj.transform.position.y < 3)
        {
            if (!spawnedLast)
            {
                spawnedLast = true;
                obj.transform.position =  new Vector3(obj.transform.position.x, 3, 0);
            }
            else
            {
                obj.transform.position =  Vector3.zero;
            }
        }
        pickUps_list[selected].Remove(obj);
        pickUps_listInUse.Add(obj);
    }
    
    float FuturePosition(float time)
    {
        // Programming Game AI by Example - Mat Buckland
        // https://answers.unity.com/questions/1087568/3d-trajectory-prediction.html
 
        // Starting position of the ball
        Vector2 pPos = rb.position;
 
        // Drag multiplier (friction)
        float rDrag = Mathf.Clamp01(1.0f - (rb.drag * Time.fixedDeltaTime));
 
        // How much velocity is added per frame
        Vector2 velocityPerFrame = rb.velocity;
         
        // How many frames are going to pass in the given time
        for(int i = 0; i < time/Time.fixedDeltaTime; i++)
        {
            velocityPerFrame *= rDrag;
            pPos += (velocityPerFrame * Time.fixedDeltaTime);
        }
 
        return pPos.x;
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
        else if (element.GetComponent<PidgeonPickUp>() != null)
        {
            return 1;
        }
        else
        {
            return 3;
        }
    }

    void ClearInUse()
    {
        foreach (GameObject element in pickUps_listInUse)
        {
            if (element.GetComponent<PowerUp_Base>() != null)
            {
                if (element.GetComponent<PowerUp_Base>().GetType() == PowerUpType.chili)
                {
                    pickUps_list[0].Add(element);
                }
                else
                {
                    pickUps_list[2].Add(element);
                }
            }
            else if (element.GetComponent<PidgeonPickUp>() != null)
            {
                pickUps_list[1].Add(element);
            }
            else
            {
                pickUps_list[3].Add(element);
            }
        }
        
        pickUps_listInUse.Clear();
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
