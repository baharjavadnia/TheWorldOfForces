// Assets/Scripts/UIManager.cs
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for UI Button

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI energyBallCounterText; // Assign in Inspector
    public GameObject congratulationsText; // Assign in Inspector
    public GameObject levelStartText; // Assign in Inspector
    public GameObject gameOverPanel; // Reference to Game Over panel in the UI
    public AudioSource backgroundMusic; // Assign the background music AudioSource

    // References for the buttons
    public Button restartButton; // Assign in Inspector
    public Button exitButton; // Assign in Inspector

    private int energyBallCount = 0;

    void Start()
    {
        UpdateEnergyBallCounter();
        if (levelStartText != null)
        {
            levelStartText.SetActive(true);
            Invoke("HideLevelStartText", 3f); // Hide after 3 seconds
        }
        if (congratulationsText != null)
        {
            congratulationsText.SetActive(false); // Ensure this is hidden at the start
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Ensure game over panel is hidden at start
        }

        // Add listeners for button clicks
        restartButton.onClick.AddListener(RestartLevel);
        exitButton.onClick.AddListener(ExitLevel);
    }

    public void AddEnergyBall()
    {
        energyBallCount++;
        UpdateEnergyBallCounter();

        // Check if energy balls have reached the limit
        if (energyBallCount >= 4) 
        {
            EndGame(true); // Win condition
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            HandleArrowInput();
        }
    }

    void HideLevelStartText()
    {
        if (levelStartText != null)
        {
            levelStartText.SetActive(false);
        }
    }

    private void UpdateEnergyBallCounter()
    {
        if (energyBallCounterText != null)
        {
            energyBallCounterText.text = energyBallCount.ToString();
        }
    }

    private void EndGame(bool isWin)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        Debug.Log("EndGame called with isWin: " + isWin);

        if (isWin)
        {
            // Show congratulations text for winning
            if (congratulationsText != null)
            {
                congratulationsText.SetActive(true);
            }
        }
        else
        {
            // Show game over panel for losing
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }

        // Ensure buttons are visible and functional
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
            restartButton.interactable = true; // Ensure button is interactable
        }
        if (exitButton != null)
        {
            exitButton.gameObject.SetActive(true);
            exitButton.interactable = true; // Ensure button is interactable
        }

        // Use a CanvasGroup to ensure UI elements are visible even when the game is paused
        Canvas.ForceUpdateCanvases();

        // Pause the game
        Time.timeScale = 0; // Stop the game during the end state
    }

    private void HandleArrowInput()
    {
        Debug.Log("Arrow key pressed. Implement button action here.");
    }

    // Restart the level
    private void RestartLevel()
    {
        Debug.Log("Restart button clicked."); // Debug message for button click
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Exit the level
    private void ExitLevel()
    {
        Debug.Log("Exit button clicked."); // Debug message for button click
        Application.Quit();
        Debug.Log("Exit Application");
    }

    // Call this method for losing the game
    public void TriggerGameOver()
    {
        EndGame(false); // Lose condition
    }

    private void DebugUIState()
    {
        Debug.Log("CongratulationsText active: " + (congratulationsText != null && congratulationsText.activeSelf));
        Debug.Log("RestartButton active: " + (restartButton != null && restartButton.gameObject.activeSelf));
        Debug.Log("ExitButton active: " + (exitButton != null && exitButton.gameObject.activeSelf));
    }
}