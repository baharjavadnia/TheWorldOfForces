using UnityEngine;

public class FloatingBall : MonoBehaviour
{
    public float floatSpeed = 1f; // Speed of floating
    public float floatHeight = 0.5f; // Height of floating
    public float rotationSpeed = 50f; // Speed of rotation

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Save the initial position
    }

    void Update()
    {
        // Floating animation
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Rotation animation
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}