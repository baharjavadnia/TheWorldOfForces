// Assets/Scripts/EnergyBall.cs
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public AudioSource earnSound; // Assign the earn sound AudioSource
    private UIManager uiManager; // Reference to the UIManager

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>(); // Find the UIManager in the scene
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene! Make sure it's present.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            // Play the earn sound
            if (earnSound != null)
            {
                earnSound.Play();
                Debug.Log("EnergyBall: Earn sound played.");
            }

            // Notify the UIManager to update the count
            if (uiManager != null)
            {
                uiManager.AddEnergyBall();
            }

            // Destroy the EnergyBall after the sound finishes playing
            Destroy(gameObject, earnSound != null ? earnSound.clip.length : 0f);

            Debug.Log("EnergyBall: Energy ball collected!");
        }
    }
}