using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3[] points = new Vector3[2];

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }

    public void SetUp(Vector3[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }
}
