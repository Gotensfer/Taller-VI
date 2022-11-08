using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllData : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt($"ResetForRelease101", -1) == -1)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt($"ResetForRelease101", 0);
        }
    }
}