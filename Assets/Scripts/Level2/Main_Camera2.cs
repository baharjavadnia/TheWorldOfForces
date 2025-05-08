using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera2 : MonoBehaviour
{
    private Player2 Player;
    [SerializeField]
    private GameObject Ground;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player2").GetComponent<Player2>();
        Ground = GameObject.Find("Ground").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z - 8f > transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Player.transform.position.z - 8f);
        } 
    }
}
