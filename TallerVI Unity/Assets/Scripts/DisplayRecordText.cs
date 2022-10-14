using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayRecordText : MonoBehaviour
{
    TextMeshProUGUI recordText;

    private void Start()
    {
        recordText = GetComponent<TextMeshProUGUI>();
        recordText.text = $"{Mathf.Round(PlayerPrefs.GetFloat("Distance", 0))}m";
    }

}
