using UnityEngine;

public class KillboxTrigger : MonoBehaviour
{
    public PlayerController player;

    public GameObject cameraPos;

    public CheckpointTrigger activeCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.transform.position = player.spawnPos;

        cameraPos.transform.position = activeCheckpoint.areaPOS;

    }

}
