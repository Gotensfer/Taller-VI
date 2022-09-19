using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalExplotion_Module : MonoBehaviour
{
    [SerializeField] GameObject FinalExplotionVFX;

    public void Explode()
    {
        Instantiate(FinalExplotionVFX, transform.position + Vector3.up*2, Quaternion.identity);
    }
}
