using UnityEngine;
using TMPro;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public float mouseSensitivity = 60f;
    public float mouseSmoothTime = 0.05f;
    public Transform playerCamera;
    public float lookUpLimit = 80f;
    public float lookDownLimit = 80f; // filepath: c:\VR4\TheForceWorld\Assets\Scripts\FirstPersonController.cs
    public TextMeshProUGUI gearInteractText; // For the gear
    public TextMeshProUGUI lightSwitchInteractText; // For the light switch
    public AudioSource jumpSound; // Assign the jump sound AudioSource
    public AudioSource footstepSound; // Assign the footstep sound AudioSource

    public float fallThreshold = -10f; // Set threshold for fall detection
    private GameOverManager gameOverManager;
    
    private Rigidbody rb;
    private bool isGrounded;
    private float cameraPitch = 0f;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerCamera != null)
        {
            playerCamera.localPosition = new Vector3(0f, 1.6f, 0f);
            playerCamera.localEulerAngles = Vector3.zero;
        }

        gameOverManager = FindObjectOfType<GameOverManager>(); // Find the GameOverManager instance
    }

    void Update()
    {
        Look();
        Move();
        
        // Check if the player's Y position is below the fall threshold
        if (transform.position.y < fallThreshold)
        {
            TriggerGameOver();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Look()
    {
        if (playerCamera == null)
        {
            Debug.LogError("PlayerCamera is not assigned!");
            return;
        }

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        Vector2 targetMouseDelta = new Vector2(mouseX, mouseY) * mouseSensitivity;
        currentMouseDelta = Vector2.Lerp(currentMouseDelta, targetMouseDelta, 1f - Mathf.Exp(-1f / mouseSmoothTime * Time.deltaTime));

        // Yaw (horizontal) on player object
        transform.Rotate(Vector3.up * currentMouseDelta.x);

        // Pitch (vertical) on camera only
        cameraPitch -= currentMouseDelta.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -lookDownLimit, lookUpLimit);
        playerCamera.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = (transform.right * horizontal + transform.forward * vertical).normalized;
        Vector3 velocity = direction * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        if (footstepSound != null && !footstepSound.isPlaying)
        {
            footstepSound.Play(); // Play the footstep sound
            Debug.Log("Player: Played footstep sound.");
        }
    }

    private void Jump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (jumpSound != null)
            {
                jumpSound.Play(); // Play the jump sound
                Debug.Log("Player: Played jump sound.");
            }
        }
    }

    private void TriggerGameOver()
    {
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver(); // Call show game over from GameOverManager
        }
    }
}