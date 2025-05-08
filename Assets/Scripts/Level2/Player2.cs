using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    private Rigidbody rb;
    private float HInput;
    private float VInput;
    private Vector3 Control;
    public Vector3 newGroundPos;
    private Ground_Manager GM;
    private GameObject pastGround;
    [SerializeField] private GameObject Energy;
    private float zarib = 1.2f;
    private float gap = 100;
    private float EZpos = 100;
    private Vector3 EPose;
    public int Energy_Score = 0;
    private bool Win = false;
    [SerializeField] private GameObject door;
    [SerializeField] private UI_Manager2 I;
    private Vector3 stopPos;
    [SerializeField] private AudioSource get;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GM = GameObject.Find("Ground_Manager").GetComponent<Ground_Manager>();
        EPose = new Vector3(Random.Range(-5.5f, 5.5f), -0.5f, EZpos);
        Instantiate(Energy, EPose, Quaternion.identity);
        I = GameObject.Find("UI_Manager2").GetComponent<UI_Manager2>();
    }

    // Update is called once per frame
    void Update()
    {
        HInput = Input.GetAxis("Horizontal");

        Control = new Vector3(HInput  * 3.9f, 0, 0.1f);
        rb.AddForce(Control , ForceMode.Acceleration);
        
        if (Energy_Score == 4 && Win == false)
        {
            Win = true;
            Instantiate(door, new Vector3(0, -1 ,transform.position.z + 200f), Quaternion.Euler( new Vector3 (0, 90, 0)));
        }
        if (transform.position.y < -4f)
        {
            I.lose();
        }

    }
    private void OnCollisionExit(Collision other)
    {
        if ( other.gameObject.tag == "Ground")
        {
            pastGround = other.gameObject;
            newGroundPos = pastGround.transform.position;
            StartCoroutine(DestroyGround());
            GM.Create();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Rock")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            I.lose();
        }
    }

    IEnumerator DestroyGround()
    {
        yield return new WaitForSeconds(5f);
        Destroy(pastGround);
    }
    public void openDoor()
    {
        Energy_Score++;
        EZpos = transform.position.z + (zarib * gap);
        Instantiate(Energy, new Vector3(Random.Range(-5.5f, 5.5f), -0.5f, EZpos), Quaternion.identity);
        gap = gap * zarib;
        I.adscore2();
        get.Play();
    }
    public void Stop(Vector3 x)
    {
        transform.position = x;
    }
    public void emake()
    {

    }

}
