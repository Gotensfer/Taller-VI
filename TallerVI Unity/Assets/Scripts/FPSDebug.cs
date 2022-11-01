using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSDebug : MonoBehaviour
{
    int avgFrameRate;
    TextMeshProUGUI display_Text;
    float current = 0;

    private void Start()
    {
        display_Text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {        
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        display_Text.text = $"{avgFrameRate} FPS";
    }
}
