using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController pl1;
    public Player2 pc2;
  


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Trap" && gameObject.tag=="Player1")
        {
            //player1 dies
            Debug.Log("Player1 dies");
            pl1.Player1Death();
        }

        if (collision.tag == "Trap" && gameObject.tag == "Player2")
        {
            //player1 dies
            Debug.Log("Player2 dies");
            pc2.Player2Death();
        }


    }

}
