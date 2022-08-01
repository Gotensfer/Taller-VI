using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + new Vector3(8, 2, -10);
    }
}
