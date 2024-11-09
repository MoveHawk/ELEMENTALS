//using UnityEngine;
//using UnityEngine.Events;

//public class TriggerZone : MonoBehaviour
//{
//    public bool oneShot = false;
//    private bool alreadyEntered = false;
//    private bool alreadyExited = false;

//    public string collisionTag;
//    public string collisionTag2;
//    public UnityEvent onTriggerEnter;
//    public UnityEvent onTriggerExit;

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (alreadyEntered)
//            return;

//        if (!string.IsNullOrEmpty(collisionTag) && !string.IsNullOrEmpty(collisionTag2) && !collision.CompareTag(collisionTag) && !collision.CompareTag(collisionTag2))
//            return;

//        onTriggerEnter?.Invoke();

//        if (oneShot)
//            alreadyEntered = true;
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (alreadyExited)
//            return;

//        if (!string.IsNullOrEmpty(collisionTag) && !string.IsNullOrEmpty(collisionTag2) && !collision.CompareTag(collisionTag) && !collision.CompareTag(collisionTag))
//            return;

//        onTriggerExit?.Invoke();

//        if (oneShot)
//            alreadyExited = true;
//    }
//}

using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public bool oneShot = false;
    private bool alreadyEntered = false;
    private bool alreadyExited = false;

    public string collisionTag1; // Tag for the first player
    public string collisionTag2; // Tag for the second player
    public UnityEvent onBothPlayersEnter; // Event for both players entering
    public UnityEvent onBothPlayersExit; // Event for both players exiting

    private int playersInside = 0; // Counter for players inside the trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider has a valid tag
        if (!string.IsNullOrEmpty(collisionTag1) && !collision.CompareTag(collisionTag1) &&
            !string.IsNullOrEmpty(collisionTag2) && !collision.CompareTag(collisionTag2))
            return;

        // Increment the players inside counter
        playersInside++;

        // Check if both players are inside
        if (playersInside == 2)
        {
            onBothPlayersEnter?.Invoke(); // Invoke the event if both players are inside
        }

        // Prevent multiple triggers if oneShot is enabled
        if (oneShot && alreadyEntered)
            return;

        alreadyEntered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the collider has a valid tag
        if (!string.IsNullOrEmpty(collisionTag1) && !collision.CompareTag(collisionTag1) &&
            !string.IsNullOrEmpty(collisionTag2) && !collision.CompareTag(collisionTag2))
            return;

        // Decrement the players inside counter
        playersInside--;

        // Check if both players have exited
        if (playersInside < 2 && playersInside >= 0)
        {
            onBothPlayersExit?.Invoke(); // Invoke the event if both players have exited
        }

        // Prevent multiple triggers if oneShot is enabled
        if (oneShot && alreadyExited)
            return;

        alreadyExited = true;
    }
}