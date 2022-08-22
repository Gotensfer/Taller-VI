using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTest : MonoBehaviour
{
    [Range(0, 1), SerializeField] float rateOfParallax;
    Vector3 initialOffset;

    private void Start()
    {
        initialOffset = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = (new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0) * rateOfParallax);
    }
}
