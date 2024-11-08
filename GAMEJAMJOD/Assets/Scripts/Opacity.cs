using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Opacity : MonoBehaviour
{
    public Light2D spotlight;         // Reference to the Light2D component
    [Range(0, 1)] public float minAlpha = 0.3f;  // Minimum alpha value
    [Range(0, 1)] public float maxAlpha = 1.0f;  // Maximum alpha value
    public float fluctuationSpeed = 1.0f;         // Speed of fluctuation

    private float alphaDirection = 1.0f;          // Direction of alpha change
    private float currentAlpha;                   // Current alpha value

    void Start()
    {
        // Initialize spotlight and currentAlpha based on the initial color
        if (spotlight == null) spotlight = GetComponent<Light2D>();
        currentAlpha = spotlight.color.a;
    }

    void Update()
    {
        // Update the alpha value in the specified direction
        currentAlpha += alphaDirection * fluctuationSpeed * Time.deltaTime;

        // Reverse direction if we reach min or max alpha
        if (currentAlpha >= maxAlpha)
        {
            currentAlpha = maxAlpha;
            alphaDirection = -1.0f;
        }
        else if (currentAlpha <= minAlpha)
        {
            currentAlpha = minAlpha;
            alphaDirection = 1.0f;
        }

        // Apply the new alpha to the spotlight color
        Color spotlightColor = spotlight.color;
        spotlightColor.a = currentAlpha;
        spotlight.color = spotlightColor;
    }
}
