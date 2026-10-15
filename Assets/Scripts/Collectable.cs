using UnityEngine;

public class Collectable : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = ServiceHub.Instance.playerController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.coins++;
        Destroy(this.gameObject);
    }
}
