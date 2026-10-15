using UnityEngine;

public class MovingPlatformTrigger : MonoBehaviour
{
    public GameObject platform;

    public MovingPlatform movingPlatform;

    public PlayerController playerController;

    public Transform offPointDown;
    public Transform offPointUp;

    public bool targetDestinationDown;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered");

            playerController.enabled = false;

            collision.gameObject.transform.position = platform.transform.position;

            if (collision.gameObject.transform.position.y > movingPlatform.resetPoint.y)
            {
                targetDestinationDown = true;
            }

            else
            {
                targetDestinationDown = false;
            }
     
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.enabled = false;

            //Vector3 movement = Vector3.MoveTowards(collision.gameObject.transform.position, platform.transform.position, 0.1f);
            //collision.gameObject.transform.position = movement;

            collision.gameObject.transform.position = platform.transform.position;

            

            if (targetDestinationDown)
            {
                if (movingPlatform.atPointB)
                {
                    collision.gameObject.transform.position = offPointDown.position;
                }
            }

            else
            {
                if (movingPlatform.atPointA)
                {
                    collision.gameObject.transform.position = offPointUp.position;
                }
            }
            
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        playerController.enabled = true;
        
    }
}
