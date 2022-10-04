using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllData : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt($"Reset", -1) == -1) // 2k
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt($"Reset", 0);
        }
    }
}
