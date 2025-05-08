using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float HorizInput;
    private float VertiInput;
    private Animator walk;
    public int EnergyScore = 0;
    [SerializeField] private GameObject door;
    private Main_Camera Cam;
    public Vector3 stopCam;
    private Obstacle_Manager OM;
    private UI_Manager U;
    [SerializeField] private AudioSource mu;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        walk = GetComponent<Animator>();
        Cam = GameObject.Find("Main_Camera").GetComponent<Main_Camera>();
        stopCam = Cam.transform.position;
        OM = GameObject.Find("Obstacle_Manager").GetComponent<Obstacle_Manager>();
        U = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();

    }
    // Update is called once per frame
    void Update()
    {
        HorizInput = Input.GetAxis("Horizontal");
        VertiInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(HorizInput * 2f , VertiInput * 2f, 0);
        if (rb.velocity.x != 0)
        {
            walk.enabled = true;
        }
        else
        {
            walk.enabled = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(rb.velocity.x, 300f, 0));
        }

        if (transform.position.y > 5 || transform.position.y < -5)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, 0);
        }
       

    }
    public void AddEnergy()
    {
        EnergyScore++;
        mu.Play();
        U.adscore();
        if (EnergyScore == 4)
        {
            stopCam = new Vector3(transform.position.x + 24f, 0, 0);
            Instantiate(door, stopCam, Quaternion.identity);
            Cam.check = true;
            OM.stopsp = true;
        }
        
    }
}
