using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerBase_Module : MonoBehaviour
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

    //Agregue esta funcion simplemente para hacer mas facil el hecho de sacar el vector2, pero si es innecesario se borra
    public Vector3 TrackPlayer()
    {
        return player.transform.position;
    }
}
