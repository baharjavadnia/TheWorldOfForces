using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isMoving = false;

    public AudioSource doorSound; // Assign the door sound AudioSource

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        if (isMoving)
        {
            Quaternion targetRotation = isOpen ? openRotation : closedRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            
            // Check if we're close enough to the target rotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation; // Snap to exact rotation
                isMoving = false;
            }
        }
    }

    public void OpenDoor()
    {
        isOpen = !isOpen; // Toggle the door state
        isMoving = true;
        Debug.Log("Door state toggled to: " + (isOpen ? "Open" : "Closed"));

        if (doorSound != null)
        {
            doorSound.Play(); // Play the door sound
            Debug.Log("Door: Played door toggle sound.");
        }
    }
}