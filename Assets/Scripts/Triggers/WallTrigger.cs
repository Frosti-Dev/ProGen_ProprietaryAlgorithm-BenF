using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    public GameObject wall;
    public BoxCollider2D wallCollider;
    public CameraShake cameraShake;
    private Vector3 targetPosition;
    private float duration;


    private void Awake()
    {
        targetPosition = new Vector3(0,-6,0);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(cameraShake.Shake(2f, 0.05f));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            duration += Time.deltaTime;

            wallCollider.enabled = false;
            wall.transform.position = Vector3.Lerp(wall.transform.position, targetPosition, duration * Time.deltaTime);
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        wall.SetActive(false);
    }

}
