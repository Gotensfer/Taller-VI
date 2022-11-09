using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllData : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt($"ResetForRelease101k", -1) == -1)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt($"ResetForRelease101k", 0);
        }
    }
}