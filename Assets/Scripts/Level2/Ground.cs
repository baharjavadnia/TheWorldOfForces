using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Rocks = new GameObject[8];
    private float groundWidth = 12f;
    private float groundLength = 100f; 

    void Start()
    {
        int rockCount = Random.Range(4, 8);

        List<int> usedIndices = new List<int>();
        for (int i = 0; i < rockCount; i++)
        {
            int rockIndex = Random.Range(0, Rocks.Length);
            // while (usedIndices.Contains(rockIndex)) rockIndex = Random.Range(0, Rocks.Length);
            // usedIndices.Add(rockIndex);

            GameObject rockPrefab = Rocks[rockIndex];
            if (rockPrefab == null) continue;

            float xPos = Random.Range(-((groundWidth-0.5f) / 2f), (groundWidth - 0.5f) / 2f);
            float zPos = Random.Range(-((groundLength - 0.5f) / 2f), (groundLength - 0.5f) / 2f);
            Vector3 spawnPos = transform.position + new Vector3(xPos, 0.2f, zPos);

            Instantiate(rockPrefab, spawnPos, Quaternion.identity);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
