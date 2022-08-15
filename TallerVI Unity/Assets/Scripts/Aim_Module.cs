using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_Module : MonoBehaviour
{
    public Vector3 direction = new Vector3(1,0,0);
    [SerializeField, Range(0, (90*Mathf.PI/180))] 
    private float nose;
    
    void Update()
    {
        Aim();
    }

    public void Aim()
    {
        direction = new Vector3(Mathf.Cos(nose),Mathf.Sin(nose)).normalized;
        Debug.DrawLine(new Vector3(0,0,0),direction, Color.blue);
    }
}
