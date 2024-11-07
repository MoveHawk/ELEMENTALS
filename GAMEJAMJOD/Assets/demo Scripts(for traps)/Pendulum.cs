using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movespeed=20.0f;
    public float rightAngle;
    public float leftAngle;
    public bool moveclockWise;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        moveclockWise= true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.rotation.z);
        PendulumMove();
    }

    void ChangeMoveDir()
    {
        if (transform.rotation.z > rightAngle)
        {
            moveclockWise = false;
        }

        else if (transform.rotation.z < leftAngle)
        {
            moveclockWise = true;
        }
    }

    void PendulumMove()
    {
        ChangeMoveDir();
        if (moveclockWise) 
        {
            rb.angularVelocity = movespeed;
        }

        else if(!moveclockWise) 
        {
            rb.angularVelocity = -1 * movespeed;
        }
    }
}
