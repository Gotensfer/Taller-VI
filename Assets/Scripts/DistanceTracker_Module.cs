using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTracker_Module : MonoBehaviour
{
    public float travelledDistance { private set; get; }
    private Vector2 origin;
    [SerializeField] private Transform player; //despues se cambia por la funcion get player

    void Start()
    {
        origin = player.position;//getplayer()
        origin.y = 0;
        travelledDistance = 0;
    }

    void Update()
    {
        Debug.Log(CalculateTravelledDistance(Vector2.zero, player.position));//getPlayer()
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
}
