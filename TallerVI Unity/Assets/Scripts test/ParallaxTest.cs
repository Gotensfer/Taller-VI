using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTest : MonoBehaviour
{
    [SerializeField] float rateOfParallax;
    [SerializeField] Transform target;
    Vector3 initialOffset;

    private void Start()
    {
        initialOffset = target.position - transform.position; 
    }

    private void Update()
    {
        transform.position = (target.position * rateOfParallax) - initialOffset;
    }
}
