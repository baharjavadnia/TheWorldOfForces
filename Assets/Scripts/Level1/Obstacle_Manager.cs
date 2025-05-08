using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs = new GameObject[7];
    [SerializeField] private GameObject energyPrefab;
    private float lastSpawnX =-4f;
    private float cameraX;
    private bool isHighPosition = true; // To alternate between high and low positions
    public float yPosition;
    private Vector3[] R = new Vector3[7];
    private int[] theR = new int[3];
    private float nextEnergyX = 20f;
    public bool stopsp = false;
    private UI_Manager ui;

    private void Start()
    {
        theR = new int[3] {3 ,5 ,6 };
        R = new Vector3[] { new Vector3(0, 0, 180),
        new Vector3(0, 0, 180),
        new Vector3(-90, 0, -200),
        new Vector3(0,-60, 0),
        new Vector3(0,-60, 0),
        new Vector3(0, 120, 0),
        new Vector3(0, 120, 0)};
        ui = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
    }
    void FixedUpdate()
    {
        // Get camera's X position
        cameraX = Camera.main.transform.position.x;

        // Check if we should spawn new obstacles
        if (cameraX + 10f > lastSpawnX && stopsp == false)
        {
            SpawnObstacle();
        }

        // Spawn energy every 20 units in x
        if (cameraX >= nextEnergyX && stopsp == false)
        {
            float randomY = Random.Range(-6f, 6f); // Random y position
            Vector3 spawnPos = new Vector3(nextEnergyX, randomY, 0f);
            Instantiate(energyPrefab, spawnPos, Quaternion.identity);
            nextEnergyX += 20f;
        }
    }

    private void SpawnObstacle()
    {
        // Randomly select an obstacle from the array
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject selectedPrefab = obstaclePrefabs[randomIndex];

        if (selectedPrefab != null)
        {
            // Calculate random X spacing between 0.9 and 1.2
            float xSpacing = Random.Range(2.8f, 3.8f);
            float newX = lastSpawnX + xSpacing;

            // Calculate Y position (alternating between 8 and -8)
            yPosition = isHighPosition ? 7f : -7f;
            isHighPosition = !isHighPosition;

            // Create position vector
            Vector3 spawnPosition = new Vector3(newX, yPosition, 0f);

            // Instantiate the obstacle with zero rotation first
            GameObject newObstacle = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            newObstacle.transform.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

            // Set position first
            newObstacle.transform.position = spawnPosition;
            
            // Then apply rotation
            if ((randomIndex == 2) | (randomIndex == 5))
            {
                newObstacle.transform.rotation = Quaternion.Euler(R[6]);
            }
            else
            {
                newObstacle.transform.rotation = Quaternion.Euler(R[4]);
            }
            

            // Apply random 
            float randomScale = Random.Range(0.8f, 1f);
            newObstacle.transform.localScale *= randomScale;
            lastSpawnX = newX;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            ui.lose();
            Debug.Log("u");
        }
    }
} 