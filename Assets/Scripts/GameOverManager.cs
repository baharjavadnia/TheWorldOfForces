// Assets/Scripts/GameOverManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign in Inspector
    public TextMeshProUGUI gameOverText; // Assign in Inspector

    void Start()
    {
        HideGameOverPanel(); // Ensure game over panel is hidden at the start
    }

    public void ShowGameOver()
    {
        if (gameOverPanel == null || gameOverText == null)
        {
            Debug.LogError("GameOverPanel or GameOverText is not assigned!");
            return;
        }

        Time.timeScale = 0; // Pause the game
        gameOverPanel.SetActive(true); // Activate the Game Over panel
        gameOverText.text = "OH NO... GAME OVER!"; // Update the text
    }

    public void RestartLevel()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void ExitGame()
    {
        Time.timeScale = 1; // Resume the game before quitting
        Application.Quit(); // Exit the application
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
        #endif
    }

    public void HideGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Hide the panel
        }
    }
}