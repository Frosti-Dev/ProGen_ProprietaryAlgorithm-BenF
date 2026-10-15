using UnityEngine;
public class LevelChangeTrigger : MonoBehaviour
{
    public LevelManager levelManager;

    public GameObject levelToActivate;
    public Transform spawnLocation;

    private void Start()
    {
        levelManager = ServiceHub.Instance.levelManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player entered level change trigger");

            levelManager.LevelChange(levelToActivate, spawnLocation);
        }
    }
}
