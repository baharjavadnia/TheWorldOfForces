using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject Lose;
    private Player player;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void lose()
    {
        Lose.SetActive(true);
    }
    public void win()
    {
        Win.SetActive(true);
    }
    public void adscore()
    {
        Score.text = player.EnergyScore.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource یا AudioClip تنظیم نشده است!");
            }
        }
    }
}
