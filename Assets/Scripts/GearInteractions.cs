using UnityEngine;

public class GearInteractions : MonoBehaviour
{
    public AudioSource gearSound; // Assign the gear sound AudioSource

    void RotateGear()
    {
        Debug.Log("Gear is rotating."); // Add this to confirm the method is called

        // ...existing rotation logic...

        if (gearSound != null && !gearSound.isPlaying)
        {
            gearSound.Play(); // Play the gear sound
            Debug.Log("Gear: Played gear rotation sound.");
        }
    }
}