using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DisplayRecordText : MonoBehaviour
{
    TextMeshProUGUI recordText;

    private void Start()
    {
        recordText = GetComponent<TextMeshProUGUI>();
        recordText.text = $"{Math.Round(PlayerPrefs.GetFloat("Distance", 0), 1)}m";
    }

}
