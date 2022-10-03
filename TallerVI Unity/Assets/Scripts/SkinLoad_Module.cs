using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoad_Module : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController kkawaiSkinOverride;

    private void Start()
    {
        if (SkinData.ID == SkinID.Kkawai)
        {
            GetComponent<Animator>().runtimeAnimatorController = kkawaiSkinOverride;
        }
    }
}
