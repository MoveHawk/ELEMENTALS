using UnityEngine;

public class Player1Collectible : MonoBehaviour
{
    public bool hasCollectedDiamond = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diamond"))
        {
            hasCollectedDiamond = true;
            Destroy(other.gameObject);
            // Notify LevelManager that Player 1 has collected their diamond
            LevelManager.Instance.OnPlayerCollectedDiamond(1);
        }
    }
}
