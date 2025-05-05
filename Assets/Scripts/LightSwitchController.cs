using UnityEngine;
using TMPro;

public class LightSwitchController : MonoBehaviour
{
    public Light pointLight; // Assign your Point Light here
    public Transform switchButton; // Assign the button part of the switch (the moving part)
    public float offAngle = -130f;
    public float onAngle = -50f;
    public float animationSpeed = 200f; // Degrees per second
    public TextMeshProUGUI interactText; // Assign your TMP UI Text here
    public Transform player; // Assign the player transform
    public float interactDistance = 2.5f; // Distance to interact
    public AudioSource switchSound; // Assign in Inspector

    private bool isPlayerNear = false;
    private bool isOn = false;
    private bool isAnimating = false;

    void Start()
    {
        // Initialize the light and switch state
        isOn = false;
        SetSwitchRotation(offAngle);
        if (pointLight != null) pointLight.enabled = false;
        if (interactText != null) interactText.enabled = false;

        Debug.Log("LightSwitchController: Initialized. Light is OFF.");
    }

    void Update()
    {
        // Check distance between player and switch
        float dist = Vector3.Distance(player.position, transform.position);
        isPlayerNear = dist < interactDistance;

        // Show interaction text if player is near
        if (isPlayerNear)
        {
            if (interactText != null && !interactText.enabled)
            {
                interactText.enabled = true;
                interactText.text = isOn ? "Turn Off (Press Enter)" : "Turn On (Press Enter)";
                Debug.Log($"LightSwitchController: Player near. Showing text: {interactText.text}");
            }

            // Handle interaction when the player presses Enter
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ToggleLight(); // Allow toggling without blocking
            }
        }
        else if (!isPlayerNear && interactText != null && interactText.enabled)
        {
            // Hide the text if the player is not near
            interactText.enabled = false;
            Debug.Log("LightSwitchController: Player not near. Hiding interaction text.");
        }

        // Animate the switch button
        if (isAnimating && switchButton != null)
        {
            AnimateSwitch();
        }
    }

    void ToggleLight()
    {
        isOn = !isOn; // Toggle the light state
        if (pointLight != null)
        {
            pointLight.enabled = isOn; // Toggle the light
            Debug.Log($"LightSwitchController: Light turned {(isOn ? "ON" : "OFF")}.");
        }

        // Play switch sound
        if (switchSound != null)
        {
            switchSound.Play();
            Debug.Log("LightSwitchController: Played switch sound.");
        }

        // Start animating the switch button
        isAnimating = true;
    }

    void AnimateSwitch()
    {
        float targetAngle = isOn ? onAngle : offAngle;
        float currentAngle = switchButton.localEulerAngles.x;
        if (currentAngle > 180) currentAngle -= 360;
        float newAngle = Mathf.MoveTowards(currentAngle, targetAngle, animationSpeed * Time.deltaTime);
        switchButton.localEulerAngles = new Vector3(newAngle, switchButton.localEulerAngles.y, switchButton.localEulerAngles.z);

        if (Mathf.Approximately(newAngle, targetAngle))
        {
            isAnimating = false; // Stop animating when the target angle is reached
            Debug.Log($"LightSwitchController: Animation complete. Switch is now {(isOn ? "ON" : "OFF")}.");

            // Re-enable the text if the player is still near
            if (isPlayerNear && interactText != null)
            {
                interactText.enabled = true;
                interactText.text = isOn ? "Turn Off (Press Enter)" : "Turn On (Press Enter)";
                Debug.Log($"LightSwitchController: Re-enabling text: {interactText.text}");
            }
        }
    }

    void SetSwitchRotation(float angle)
    {
        if (switchButton != null)
        {
            Vector3 euler = switchButton.localEulerAngles;
            euler.x = angle;
            switchButton.localEulerAngles = euler;
            Debug.Log($"LightSwitchController: Set switch rotation to {angle} degrees.");
        }
    }
}