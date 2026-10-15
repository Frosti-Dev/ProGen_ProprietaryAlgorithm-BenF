using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public PlayerController player;
    public KillboxTrigger killbox;
    public Vector3 areaPOS;
    public TransitionTrigger transitionTrigger;

    public bool isActive;

    private SpriteRenderer sprite;
    

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        killbox = FindAnyObjectByType<KillboxTrigger>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.spawnPos = gameObject.transform.position;
        transitionTrigger.SkippedTriggerFix();
    }

    private void Update()
    {
        if (player.spawnPos == gameObject.transform.position)
        {
            isActive = true;
            killbox.activeCheckpoint = this;
        }

        else
        {
            isActive = false;
        }

        if (isActive)
        {
            sprite.color = Color.green;
        }

        else
        {
            sprite.color = Color.cyan;
        }

    }

}
