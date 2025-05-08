using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House2_V : MonoBehaviour
{
    private int A;
    private int Ychance;
    private float Speed = 0.65f;
    private Obstacle_Manager OM;
    private Camera Cam;
    // Start is called before the first frame update
    void Start()
    {
        OM = GameObject.Find("Obstacle_Manager").GetComponent<Obstacle_Manager>();
        Cam = GameObject.Find("Main_Camera").GetComponent<Camera>();
        if (OM.yPosition > 0)
        {
            A = -1;
        }
        else
        {
            A = 1;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < Cam.transform.position.x + 6f)
        {
            transform.Translate(0, Speed * A * Time.deltaTime, 0);
        }
        if (transform.position.x < Cam.transform.position.x - 20f)
        {
            Destroy(this.gameObject);
        }
    }
}
