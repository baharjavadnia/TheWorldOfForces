using UnityEngine;
using TMPro;

public class GearInteraction : MonoBehaviour
{
    public DoorController door; // Assign in Inspector
    public Transform player; // Assign Niola in Inspector
    public float interactDistance = 2.5f;
    public Canvas promptCanvas; // World-space canvas with Text
    public TextMeshProUGUI promptText; // The TMP Text component on the canvas
    public float rotationSpeed = 360f; // Degrees per second
    public AudioSource gearSound; // Assign the gear sound AudioSource

    private bool playerNear = false;
    private SphereCollider sphereCollider;
    private bool isRotating = false;
    private float currentRotation = 0f;
    private bool isClockwise = true;

    void Start()
    {
        if (promptCanvas != null)
            promptCanvas.enabled = false;
            
        // Get the sphere collider
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            Debug.LogError("No SphereCollider found on the gear! Please add one.");
        }
        else
        {
            // Make sure the collider is set to trigger
            sphereCollider.isTrigger = true;
            // Set the radius to match the interaction distance
            sphereCollider.radius = interactDistance;
        }
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        playerNear = dist < interactDistance;

        if (playerNear && !isRotating)
        {
            if (promptCanvas != null)
            {
                promptCanvas.enabled = true;
                promptText.text = "Rotate (Press Enter)";
            }
        }
        else
        {
            if (promptCanvas != null)
                promptCanvas.enabled = false;
        }

        // Handle gear rotation
        if (isRotating)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            if (!isClockwise) rotationAmount = -rotationAmount;
            
            transform.Rotate(Vector3.forward * rotationAmount, Space.World);
            currentRotation += Mathf.Abs(rotationAmount);

            if (currentRotation >= 360f)
            {
                isRotating = false;
                currentRotation = 0f;
                // Ensure we end up at exactly 0 or 360 degrees
                transform.rotation = Quaternion.Euler(
                    Mathf.Round(transform.eulerAngles.x / 360f) * 360f,
                    transform.eulerAngles.y,
                    transform.eulerAngles.z);
            }
        }

        // Interact - Check for Enter key press
        if (playerNear && Input.GetKeyDown(KeyCode.Return) && !isRotating)
        {
            if (door != null)
            {
                door.OpenDoor();
                // Start gear rotation
                isRotating = true;
                isClockwise = !isClockwise; // Toggle rotation direction
                currentRotation = 0f;

                // Play the gear sound
                if (gearSound != null)
                {
                    gearSound.Play();
                    Debug.Log("Gear: Played gear rotation sound.");
                }

                Debug.Log("Door interaction triggered! Distance: " + dist);
            }
            else
            {
                Debug.LogError("Door reference is missing! Please assign the door in the inspector.");
            }
        }

        Debug.DrawRay(transform.position, transform.forward * 2, Color.blue);
        Debug.DrawRay(transform.position, transform.right * 2, Color.red);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            Debug.Log("Player entered gear interaction zone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            Debug.Log("Player exited gear interaction zone");
        }
    }
}