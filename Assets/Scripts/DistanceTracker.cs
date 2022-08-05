using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    private float _distance = 0f;
    private Vector2 origin, currentPos;
    [SerializeField] private Transform player; //despues se cambia por la funcion get player

    void Start()
    {
        origin = player.position;
    }

    void Update()
    {
        Debug.Log(TrackPlayerDistance());
    }

    string TrackPlayerDistance()
    {
        currentPos = player.position;
        _distance = Vector2.Distance(origin, currentPos);
        return _distance.ToString("0.##") + "m";
    }
}
