using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private Player2 player;
    private UI_Manager2 Ra;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player2").GetComponent<Player2>();
        Ra = GameObject.Find("UI_Manager2").GetComponent<UI_Manager2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            player.openDoor();
        }
    }

}
