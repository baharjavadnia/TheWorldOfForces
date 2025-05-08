using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    private UI_Manager2 uu;
    private Player2 ply;
    // Start is called before the first frame update
    void Start()
    {
        uu = GameObject.Find("UI_Manager2").GetComponent<UI_Manager2>();
        ply = GameObject.Find("Player2").GetComponent<Player2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            uu.win();
            ply.Stop(other.transform.gameObject.transform.position);
        }
    }
}
