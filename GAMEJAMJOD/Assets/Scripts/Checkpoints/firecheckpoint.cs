using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firecheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController gc;
   

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player1")
        {
            //update pos of fire
            Debug.Log("updated Fire checkpoint");
            gc.UpdatePlayer1Checkpoint(transform.position);
            //Debug.Log("Check kr gamecontroller attach hai kya?");
        }
    }
    }

    


