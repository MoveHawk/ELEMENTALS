using UnityEngine;
using System.Collections;

public class CoOpMechanicUnlock : MonoBehaviour
{
    public float holdDuration = 3f;      // Time in seconds both players need to hold "E" to unlock the mechanic
    public float mechanicDuration = 10f; // Duration in seconds that the mechanic remains unlocked

    public bool isMechanicActive = false;
    private bool isBothHolding = false;
    private float holdTimer = 0f;
    private float mechanicTimer = 0f;

    void Update()
    {
        // Check if both players are holding down the "E" key
        if (Input.GetKey(KeyCode.E))
        {
            isBothHolding = true;
            holdTimer += Time.deltaTime;

            // Check if they've held "E" long enough to activate the mechanic
            if (holdTimer >= holdDuration && !isMechanicActive)
            {
                ActivateMechanic();
            }
        }
        else
        {
            // Reset if either player releases the "E" key
            isBothHolding = false;
            holdTimer = 0f;
        }

        // Mechanic timer
        if (isMechanicActive)
        {
            mechanicTimer += Time.deltaTime;
            if (mechanicTimer >= mechanicDuration)
            {
                DeactivateMechanic();
            }
        }
    }

    private void ActivateMechanic()
    {
        isMechanicActive = true;
        mechanicTimer = 0f;  // Reset the mechanic timer

        Debug.Log("Mechanic Unlocked!");
        // Implement additional functionality for when the mechanic is active here
    }

    private void DeactivateMechanic()
    {
        isMechanicActive = false;
        mechanicTimer = 0f;

        Debug.Log("Mechanic Locked.");
        // Implement additional functionality for when the mechanic is deactivated here
    }
}
