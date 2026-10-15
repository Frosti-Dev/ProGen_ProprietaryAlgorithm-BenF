using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransitionTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator playerAnimController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cameraMove;

    [SerializeField] private float playerWaitTime;

    private float duration;

    [SerializeField] private bool movePlayer;

    [Header("Position Change")]
    [SerializeField] private Vector3 targetCameraPosition;
    [SerializeField] private Vector2 targetPlayerPostion;

    [Header("Animation Direction")]
    [SerializeField] private bool directionUp;
    [SerializeField] private bool directionDown;
    [SerializeField] private bool directionRight;
    [SerializeField] private bool directionLeft;

    [Header("Trigger Switch")]
    [SerializeField] private GameObject thisTrigger;
    [SerializeField] private GameObject nextTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInput.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            duration += Time.deltaTime;

            cameraMove.transform.position = Vector3.Lerp(cameraMove.transform.position, targetCameraPosition, duration * Time.deltaTime);

            if(movePlayer) StartCoroutine(WaitForCamera());

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();

        duration = 0;

        playerInput.enabled = true;
        playerController.enabled = true;

        StartCoroutine(BufferTime());
        
        
    }

    IEnumerator WaitForCamera()
    {
        yield return new WaitForSeconds(playerWaitTime);

        playerController.enabled = false;

        Debug.Log("MovePlayer");

        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPlayerPostion, 0.13f);

        playerAnimController.SetBool("isMoving", true);

        if (directionUp) playerAnimController.SetFloat("MoveInputY", 1f);
        else if (directionDown) playerAnimController.SetFloat("MoveInputY", -1f);
        else if (directionRight) playerAnimController.SetFloat("MoveInputX", 1f);
        else playerAnimController.SetFloat("MoveInputX", -1f);


    }

    IEnumerator BufferTime()
    {
        yield return new WaitForSeconds(1f);
        thisTrigger.SetActive(false);
        nextTrigger.SetActive(true);
    }

    //if trigger is avoided, this fixes the set triggers
    public void SkippedTriggerFix()
    {
        thisTrigger.SetActive(true);
        nextTrigger.SetActive(false);
    }
}
