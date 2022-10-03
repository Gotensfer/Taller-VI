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
        recordText.text = $"{PlayerPrefs.GetFloat("Distance", 0)}m";
    }

}
