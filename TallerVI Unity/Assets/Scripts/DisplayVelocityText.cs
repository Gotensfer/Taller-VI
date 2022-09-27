using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplayVelocityText : MonoBehaviour
{
    enum DisplayVelocityMode
    {
        absolute,
        horizontal,
        vertical
    }

    [SerializeField] DisplayVelocityMode displayVelocityMode;
    [SerializeField] FlyRestraint_Module flyRestraint_Module;
    [SerializeField] Rigidbody2D rb;
    TextMeshProUGUI display;

    private void Start()
    {
        display = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        switch (displayVelocityMode)
        {
            case DisplayVelocityMode.absolute:
                display.text = $"{rb.velocity.magnitude}m/s";
                break;
            case DisplayVelocityMode.horizontal:
                display.text = $"{Math.Round(rb.velocity.x)}m/s";
                break;
            case DisplayVelocityMode.vertical:
                display.text = $"{Math.Round(rb.velocity.y)}m/s";
                break;
        }
    }
}
