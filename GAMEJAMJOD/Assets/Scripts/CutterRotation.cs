using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
