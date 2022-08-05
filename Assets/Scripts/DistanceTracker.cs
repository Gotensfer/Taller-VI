using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    private float _distance = 0f;
    private Vector2 origin;
    [SerializeField] private Transform player; //despues se cambia por la funcion get player

    void Start()
    {
        origin = player.position;//getplayer()
    }

    void Update()
    {
        Debug.Log(TrackPlayerDistance(player.position));//getPlayer()
    }

    string TrackPlayerDistance(Vector2 currentPos)
    {
        _distance = Vector2.Distance(origin, currentPos);
        return _distance.ToString("0.##") + "m";
    }
}
