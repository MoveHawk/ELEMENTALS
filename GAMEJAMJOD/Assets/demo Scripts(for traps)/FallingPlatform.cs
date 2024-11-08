using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    //for falling platform
    Vector2 defaultPos;
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float respawnDelay = 1.5f;
    [SerializeField] private Rigidbody2D rb;

    public BoxCollider2D bx2d;
    public SpriteRenderer sp;

    private void Start()
    {
        defaultPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player landed on the platform, start falling
        if (collision.transform.tag == "Player")
        {
            // StartCoroutine(StartFall());
            StartCoroutine(Crumble());
        }
    }

    private IEnumerator StartFall()
    {

        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(respawnDelay);
        Reset();
    }

    private void Reset()
    {
        transform.position = defaultPos;
        rb.bodyType = RigidbodyType2D.Static;
    }

    IEnumerator Crumble()
    {
        //play anim of crumble
        yield return new WaitForSeconds(0.5f);//2.0f =anim time
        component(false);
        yield return new WaitForSeconds(1.5f);// 3.0 f respawn time
        //play anim of respawn
        component(true);
        Reset();
    }


    private void component(bool state)
    {
        bx2d.enabled = state;
        sp.enabled = state;
    }
}
