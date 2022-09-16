using System;
using UnityEngine;

public class DistanceTracker_Module : MonoBehaviour
{
    public float travelledDistance { private set; get; }
    private Vector2 origin;
    private Transform player; //despues se cambia por la funcion get player
    public float localRecord { get; private set; }

    private void Awake()
    {
        localRecord = PlayerPrefs.GetFloat("Distance", 0);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        origin.x = player.position.x;
        origin.y = 0;
        travelledDistance = 0;
    }

    private void Update()
    {
        CalculateTravelledDistance(player.position);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetRecord();
        }
#endif
    }

    public string CalculateTravelledDistance(Vector2 currentPos)
    {
        currentPos.y = 0;
        travelledDistance = Vector2.Distance(origin, currentPos);
        return travelledDistance.ToString("0.##") + "m";
    }

    public string CalculateTravelledDistance(Vector2 offset, Vector2 currentPos)
    {
        offset.y = 0;
        origin = origin + offset;
        return CalculateTravelledDistance(currentPos);
    }

    public void SetRecord()
    {
        if (travelledDistance > localRecord)
        {
            PlayerPrefs.SetFloat("Distance", travelledDistance);
            PlayerPrefs.Save();
            
            Debug.Log("New Record: " + PlayerPrefs.GetFloat("Distance"));
        }
    }

    // Para usos de debug
    void ResetRecord()
    {
        PlayerPrefs.SetFloat("Distance", 0);
        PlayerPrefs.Save();
    }
}
