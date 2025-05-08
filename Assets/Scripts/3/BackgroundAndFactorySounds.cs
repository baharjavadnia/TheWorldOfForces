using UnityEngine;

public class BackgroundAndFactorySounds : MonoBehaviour
{
    public AudioSource bgMusicSource; // Assign the AudioSource for background music
    public AudioSource factorySoundSource; // Assign the AudioSource for factory sound

    void Start()
    {
        if (bgMusicSource != null)
        {
            bgMusicSource.Play(); // Play background music
        }

        if (factorySoundSource != null)
        {
            factorySoundSource.Play(); // Play factory sound
        }
    }
}