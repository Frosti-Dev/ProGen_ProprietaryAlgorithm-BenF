using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int coins;

    public float moveSpeed = 5f;
    public Vector2 moveInput;

    private Rigidbody2D playerRb;

    private ParticleSystem particle;

    public Vector2 lastMoveInput;

    public Vector3 spawnPos;


    //Hash References for Animator
    private Animator playerAnimController;

    private int MoveInputXHash = Animator.StringToHash("MoveInputX");
    private int MoveInputYHash = Animator.StringToHash("MoveInputY");
    private int isMovingHash = Animator.StringToHash("isMoving");

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();

        playerAnimController = GetComponentInChildren<Animator>();

        particle = GetComponentInChildren<ParticleSystem>();
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        playerRb.MovePosition(playerRb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        HandlePlayerAnimations();

        GetLastMoveInput();
    }

    private void HandlePlayerAnimations()
    {
        if (moveInput != Vector2.zero)
        {
            playerAnimController.SetFloat(MoveInputXHash, moveInput.x);
            playerAnimController.SetFloat(MoveInputYHash, moveInput.y);

            playerAnimController.SetBool(isMovingHash, true);

            particle.gameObject.SetActive(true);
        }

        else
        {
            playerAnimController.SetBool(isMovingHash, false);
            particle.gameObject.SetActive(false);
        }
    }

    private void GetLastMoveInput()
    {
        if (moveInput != Vector2.zero)
        {
            //Debug.Log($"X: {lastMoveInputX}, Y {lastMoveInputY}");

            lastMoveInput = moveInput;
            
        }
    }

}
