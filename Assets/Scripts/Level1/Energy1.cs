using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy1 : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.AddEnergy();
            Destroy(this.gameObject);
        }
    }
}
