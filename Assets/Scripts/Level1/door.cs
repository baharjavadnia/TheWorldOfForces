using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    private UI_Manager ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ui.win();
        }
    }
}
