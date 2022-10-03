using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner_Module : MonoBehaviour
{
    private List<List<GameObject>> pickUps_list = new List<List<GameObject>>(4);
    private List<GameObject> pickUps_listInUse =  new List<GameObject>();
    [SerializeField] private GameObject chiliPrefab, pidegeonPrefab, rocketPrefab, mitosisPrefab, father;

    [Header("level1")]
    [SerializeField] private Vector2 rangoChili, rangoPidgeon, rangoRocket, rangoMitosis;
    [SerializeField] [Range(1, 1000)] private uint cadaCuantoRestaChili = 50, cadaCuantoRestaPidgeon = 30, cadaCuantoRestaRocket = 30, cadaCuantoRestaMitosis = 50;
    [SerializeField] [Range(0, 10)] private uint cuantoRestaChili = 1, cuantoRestaPidgeon = 2, cuantoRestaRocket = 3, cuantoRestaMitosis = 1;
    
    [Header("level2")]
    [SerializeField] private Vector2 rangoChili2, rangoPidgeon2, rangoRocket2, rangoMitosis2;
    [SerializeField] [Range(1, 1000)] private uint cadaCuantoRestaChili2 = 50, cadaCuantoRestaPidgeon2 = 30, cadaCuantoRestaRocket2 = 30, cadaCuantoRestaMitosis2 = 50;
    [SerializeField] [Range(0, 10)] private uint cuantoRestaChili2 = 1, cuantoRestaPidgeon2 = 2, cuantoRestaRocket2 = 3, cuantoRestaMitosis2 = 1;
    
    [Header("level3")]
    [SerializeField] private Vector2 rangoChili3, rangoPidgeon3, rangoRocket3, rangoMitosis3;
    [SerializeField] [Range(1, 1000)] private uint cadaCuantoRestaChili3 = 50, cadaCuantoRestaPidgeon3 = 30, cadaCuantoRestaRocket3 = 30, cadaCuantoRestaMitosis3 = 50;
    [SerializeField] [Range(0, 10)] private uint cuantoRestaChili3 = 1, cuantoRestaPidgeon3 = 2, cuantoRestaRocket3 = 3, cuantoRestaMitosis3 = 1;

    [SerializeField] private float xOffset = 0;

    //variables utilidad
    private float prevPos = 0, chance1 = 100, chance2 = 100, chance3 = 100, chance4 = 100;
    private sbyte level1, level2, level3, level4;
    private int distance, distance2, temp = 0, temp2 = 0, selected;
    private Rigidbody2D rb;
    private TrackerBase_Module track;
    private bool enabledReUse = false, gliding = false, falling = false, spawnedLast = false, enableChange = false;

    private void Awake()
    {
        LoadChance();
    }

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
                    pickUps_list[i].Add(Instantiate(chiliPrefab,new Vector3(xOffset, 0, 0), quaternion.identity,father.transform));
                }
                else if(i==1)
                {
                    pickUps_list[i].Add(Instantiate(pidegeonPrefab,new Vector3(xOffset, 0, 0), quaternion.identity,father.transform));
                }
                else if(i==2)
                {
                    pickUps_list[i].Add(Instantiate(rocketPrefab,new Vector3(xOffset, 0, 0), quaternion.identity,father.transform));
                }
                else
                {
                    pickUps_list[i].Add(Instantiate(mitosisPrefab,new Vector3(xOffset, 0, 0), quaternion.identity,father.transform));
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
            element.transform.position = new Vector3(xOffset, 0, 0);
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

            if (rb.position.y >= rangoChili.x && rb.position.y <= rangoChili.y)
            {
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

            return null;
        }
        else if (selected == 1)
        {
            float rnd = Random.Range(0, 100);

            if (rb.position.y >= rangoPidgeon.x && rb.position.y <= rangoPidgeon.y)
            {
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

            return null;
        }
        else if (selected == 2)
        {
            float rnd = Random.Range(0, 100);

            if (rb.position.y >= rangoRocket.x && rb.position.y <= rangoRocket.y)
            {
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

            return null;
        }
        else if (selected == 3)
        {
            float rnd = Random.Range(0, 100);

            if (rb.position.y >= rangoMitosis.x && rb.position.y <= rangoMitosis.y)
            {
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

            return null;
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

        if (gameObject != null)
        {
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
                    obj.transform.position =  new Vector3(xOffset, 0, 0);
                }
            }
            pickUps_list[selected].Remove(obj);
            pickUps_listInUse.Add(obj);
        }
        else
        {
            if (gliding)
            {
                GetPrevPos();
            }

            if (falling)
            {
                NegGetPrevPos();
            }
        }
    }
    
    float FuturePosition(float time)
    {
        Vector2 pPos = rb.position;
        
        float rDrag = Mathf.Clamp01(1.0f - (rb.drag * Time.fixedDeltaTime));
        
        Vector2 velocityPerFrame = rb.velocity;
        
        for(int i = 0; i < time/Time.fixedDeltaTime; i++)
        {
            velocityPerFrame *= rDrag;
            pPos += (velocityPerFrame * Time.fixedDeltaTime);
        }
 
        return pPos.x;
    }
    
    int GetListIndex(GameObject element)
    {
        if (element.GetComponent<PowerUp_Chili>() != null)
        {
            return 0;
        }
        else if(element.GetComponent<PowerUp_Rocket>() != null)
        {
            return 2;
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
            if (element.GetComponent<PowerUp_Chili>() != null)
            {
                pickUps_list[0].Add(element);
            }
            else if(element.GetComponent<PowerUp_Rocket>() != null)
            {
                pickUps_list[2].Add(element);
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
        
        if (distance % cadaCuantoRestaChili == 0)
        {
            if (distance > temp) chance1-=cuantoRestaChili;
            else if (distance < temp) chance1+=cuantoRestaChili;
        }
        
        if (distance % cadaCuantoRestaPidgeon == 0)
        {
            if (distance > temp) chance1-=cuantoRestaPidgeon;
            else if (distance < temp) chance1+=cuantoRestaPidgeon;
        }
        
        if (distance % cadaCuantoRestaRocket == 0)
        {
            if (distance > temp) chance1-=cuantoRestaRocket;
            else if (distance < temp) chance1+=cuantoRestaRocket;
        }
        
        if (distance % cadaCuantoRestaMitosis == 0)
        {
            if (distance > temp) chance1-=cuantoRestaMitosis;
            else if (distance < temp) chance1+=cuantoRestaMitosis;
        }
        
        chance1 = FixChance(chance1);
        chance2 = FixChance(chance2);
        chance3 = FixChance(chance3);
        chance4 = FixChance(chance4);
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

    void LoadChance()
    {
        level1 = (sbyte)PlayerPrefs.GetInt("Chilli Level", -1);
        level2 = (sbyte)PlayerPrefs.GetInt("Pidgeon Level", -1);
        level3 = (sbyte)PlayerPrefs.GetInt("Rocket Level", -1);
        level4 = (sbyte)PlayerPrefs.GetInt("Mitosis Level", -1);
        

        switch (level1)
        {
            case 0:
                rangoChili = Vector2.zero;
                cadaCuantoRestaChili = 1;
                cuantoRestaChili = 1;
                break;
            case 1:
                break;
            case 2:
                rangoChili = rangoChili2;
                cadaCuantoRestaChili = cadaCuantoRestaChili2;
                cuantoRestaChili = cuantoRestaChili2;
                break;
            case 3:
                rangoChili = rangoChili3;
                cadaCuantoRestaChili = cadaCuantoRestaChili3;
                cuantoRestaChili = cuantoRestaChili3;
                break;
            default:
                Debug.LogError("NO SE CARGARON LOS DATOS DE NIVEL DE Chili Power Up");
                break;
        }
        
        switch (level2)
        {
            case 0:
                rangoPidgeon = Vector2.zero;
                cadaCuantoRestaPidgeon = 1;
                cuantoRestaPidgeon = 1;
                break;
            case 1:
                break;
            case 2:
                rangoPidgeon = rangoPidgeon2;
                cadaCuantoRestaPidgeon = cadaCuantoRestaPidgeon2;
                cuantoRestaPidgeon = cuantoRestaPidgeon2;
                break;
            case 3:
                rangoPidgeon = rangoPidgeon3;
                cadaCuantoRestaPidgeon = cadaCuantoRestaPidgeon3;
                cuantoRestaPidgeon = cuantoRestaPidgeon3;
                break;
            default:
                Debug.LogError("NO SE CARGARON LOS DATOS DE NIVEL DE Pidgeon Power Up");
                break;
        }
        
        switch (level3)
        {
            case 0:
                rangoRocket = Vector2.zero;
                cadaCuantoRestaRocket = 1;
                cuantoRestaRocket = 1;
                break;
            case 1:
                break;
            case 2:
                rangoRocket = rangoRocket2;
                cadaCuantoRestaRocket = cadaCuantoRestaRocket2;
                cuantoRestaRocket = cuantoRestaRocket2;
                break;
            case 3:
                rangoRocket = rangoRocket3;
                cadaCuantoRestaRocket = cadaCuantoRestaRocket3;
                cuantoRestaRocket = cuantoRestaRocket3;
                break;
            default:
                Debug.LogError("NO SE CARGARON LOS DATOS DE NIVEL DE Rocket Power Up");
                break;
        }
        
        switch (level4)
        {
            case 0:
                rangoMitosis = Vector2.zero;
                cadaCuantoRestaMitosis = 1;
                cuantoRestaMitosis = 1;
                break;
            case 1:
                break;
            case 2:
                rangoMitosis = rangoMitosis2;
                cadaCuantoRestaMitosis = cadaCuantoRestaMitosis2;
                cuantoRestaRocket = cuantoRestaMitosis2;
                break;
            case 3:
                rangoMitosis = rangoMitosis3;
                cadaCuantoRestaMitosis = cadaCuantoRestaMitosis3;
                cuantoRestaMitosis = cuantoRestaMitosis3;
                break;
            default:
                Debug.LogError("NO SE CARGARON LOS DATOS DE NIVEL DE Mitosis Power Up");
                break;
        }
    }
}
