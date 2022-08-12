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
        travelledDistance = 0;
    }

    void Update()
    {
        Debug.Log(TrackPlayerDistance(Vector2.zero, player.position));//getPlayer()
    }

    string TrackPlayerDistance(Vector2 currentPos)
    {
        travelledDistance = Vector2.Distance(origin, currentPos);
        return travelledDistance.ToString("0.##") + "m";
    }

    string TrackPlayerDistance(Vector2 offset, Vector2 currentPos)
    {
        origin = origin + offset;
        return TrackPlayerDistance(currentPos);
    }
}
