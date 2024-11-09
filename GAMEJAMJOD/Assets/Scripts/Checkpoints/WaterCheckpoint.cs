using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCheckpoint : MonoBehaviour
{
    public GameController gc;


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
        {
            //update pos of fire
            Debug.Log("updated water checkpoint");
            // gc.UpdatePlayer2Checkpoint(transform.position);
            //Debug.Log("Check kr gamecontroller attach hai kya?");
        }
    }
}
