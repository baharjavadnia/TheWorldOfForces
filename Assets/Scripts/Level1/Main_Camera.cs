using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera : MonoBehaviour
{
    private Player player;
    public bool check = false;
    [SerializeField] private float S = 1.6f;
    // Update is called once per frame
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        transform.Translate(S * Time.deltaTime, 0, 0);

        if (check == true)
        {
            if (transform.position.x + 5.5f > player.stopCam.x)
            {
                S = 0f;
            }
        }
    }
}
