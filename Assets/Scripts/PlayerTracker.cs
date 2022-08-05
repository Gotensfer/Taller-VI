using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public Vector2 TrackPlayer()
    {
        return player.transform.position;
    }
}
