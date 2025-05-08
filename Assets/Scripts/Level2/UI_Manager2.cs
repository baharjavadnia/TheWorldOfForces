using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager2 : MonoBehaviour
{
    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject Lose;
    private Player player;
    private Player2 player2;
    [SerializeField] private TextMeshProUGUI Score;
    // Start is called before the first frame update
    void Start()
    {
        player2 = GameObject.Find("Player2").GetComponent<Player2>();
    }


    public void lose()
    {
        Lose.SetActive(true);
    }
    public void win()
    {
        Win.SetActive(true);
    }
    public void adscore2()
    {
        Score.text = player2.Energy_Score.ToString();
    }

}
