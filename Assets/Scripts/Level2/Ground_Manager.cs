using UnityEngine;

public class Ground_Manager : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    private Player2 player;
    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player2").GetComponent<Player2>();
    }

    private void Update()
    {
        // Find and destroy rocks that are behind the camera
        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
        float cameraZ = mainCamera.transform.position.z;

        foreach (GameObject rock in rocks)
        {
            if (rock.transform.position.z < cameraZ)
            {
                Destroy(rock);
            }
        }
    }

    public void Create()
    {
        Instantiate(ground, new Vector3 (player.newGroundPos.x, player.newGroundPos.y, player.newGroundPos.z +400f), Quaternion.identity);
    }
}
