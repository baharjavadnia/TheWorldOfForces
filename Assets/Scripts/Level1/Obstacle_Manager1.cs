using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Obstacle_Manager1 : MonoBehaviour
{
    private Vector3 GPos;
    private float width;
    private int heightChance;
    public int height;
    [SerializeField]
    private GameObject[] Obstacle;
    private float Scale;
    private float Z;
    private Camera Cam;
    private float lastPose;
    private Player player;
    private Vector3[] S;
    private Vector3[] R;
    // Start is called before the first frame update
    void Start()
    {
        //lastPose = 0f;
        heightChance = 0;
        Cam = GameObject.Find("Main_Camera").GetComponent<Camera>();
        //player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(setGPos());
        S =new Vector3[] { new Vector3(5, 16, 13),
        new Vector3(23, 23, 23),
        new Vector3(28, 28, 28),
        new Vector3(1, 1, 1),
        new Vector3(1.6f, 1.6f, 1.6f),
        new Vector3(0.45f, 0.45f, 0.45f),
        new Vector3(0.8f, 0.8f, 0.8f)};
        R = new Vector3[] { new Vector3(-90, 0, -90),
        new Vector3(-90, 0, 180),
        new Vector3(-90, 0, 180),
        new Vector3(0, 70, 0),
        new Vector3(-90, 155, 0),
        new Vector3(0, -60, 0),
        new Vector3(0, 180, 0)};
    }

    IEnumerator Generate()
    {
        yield return new WaitForSeconds(1.1f);
        StartCoroutine(setGPos());
    }
    IEnumerator setGPos()
    {
        width = Random.Range(Cam.transform.gameObject.transform.position.x - 6f, Cam.transform.gameObject.transform.position.x +15f);
        if (heightChance > 0)
        {
            height = Random.Range(-7, -5);
        }
        else
        {
            height = Random.Range(5, 7);
        }

        Z = Random.Range(0, 5);
        GPos = new Vector3(width, height, 0);
        GameObject newObs = Instantiate(Obstacle[Random.Range(0, Obstacle.Length)], GPos, Quaternion.identity);
        Scale = (6 - Z)* (0.25f);
        //newObs.transform.localScale = new Vector3(Scale,Scale,Scale);
        yield return new WaitForSeconds(1.1f);
        setBackGPos();
        Debug.Log("frount");
    }
    private void setBackGPos()
    {
        width = Random.Range(Cam.transform.gameObject.transform.position.x +9f, Cam.transform.gameObject.transform.position.x + 15f);
        Z = Random.Range(2, 8);
        if (heightChance%2 == 0)
        {
            height = Random.Range(-7, -5);
            heightChance++;
        }
        else
        {
            height = Random.Range(5, 7);
            heightChance++;
        }
        Scale = Random.Range(0.6f, 1f);
        GPos = new Vector3(width, height, Z);
        int SObs = Random.Range(0, Obstacle.Length);
        GameObject BackObs = Instantiate(Obstacle[SObs], GPos, Quaternion.Euler(R[SObs].x, R[SObs].y, R[SObs].z));
        //BackObs.transform.localScale = new Vector3(Scale * S[SObs].x, Scale * S[SObs].x, Scale * S[SObs].z);
        StartCoroutine(Generate());
        Debug.Log("Back");
    }
    private void Gen()
    {
        if (player.transform.position.x > lastPose + 1f)
        {
            Debug.Log((player.transform.position.x, lastPose));
            for (int i = 0; i < 2; i++)
            {
                
                heightChance = Random.Range(-2, 2);
                if (heightChance > 0)
                {
                    height = Random.Range(-7, -5);
                }
                else
                {
                    height = Random.Range(5, 7);
                }
                float x = Random.Range(transform.position.x + 9f, transform.position.x + 15f);
                Vector3 pose = new Vector3(x, height, 0);
                GameObject one = Instantiate(Obstacle[Random.Range(0, Obstacle.Length)], pose, Quaternion.identity);
                Scale = (6 - Z) * (0.25f);
                one.transform.localScale = new Vector3(Scale, Scale, Scale);

            }
            lastPose = player.transform.position.x;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
