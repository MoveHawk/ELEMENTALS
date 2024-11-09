using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player1hu pl1;
    public Player2Controller pc2;
    Vector2 Player1CheckPos;
    Vector2 Player2CheckPos;


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

    public void UpdatePlayer1Checkpoint( Vector2 pos)
    {
        Debug.Log("FirePoint Checkpoint updated");
        Player1CheckPos = pos;
    }



    public IEnumerator Player1Respawn()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = Player1CheckPos;
    }

    public void Player1Respawnneed()
    {
        StartCoroutine(Player1Respawn());
    }


    public void UpdatePlayer2Checkpoint(Vector2 pos)
    {
        Player2CheckPos = pos;
    }



    public IEnumerator Player2Respawn()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = Player2CheckPos;
    }

    public void Player2Respawnneed()
    {
        StartCoroutine(Player2Respawn());
    }
}
