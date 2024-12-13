using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Cecil.Cil;

public class GM : MonoBehaviour
{
    public static GM instance;

    [SerializeField] private GameObject _gameOverCanvas; // Canvas containing Game Over message and Restart button
    [SerializeField] private GameObject _startCanvas;    // Canvas containing the Title and Play button

    private bool isGameStarted = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        Time.timeScale = 0f; // Pause the game at the start
        _startCanvas.SetActive(true); // Show the Title and Play button at the beginning
        _gameOverCanvas.SetActive(false); // Hide Game Over canvas
    }

    private void Update()
    {
        // Start the game when the player presses the Play button or clicks the mouse
        if (!isGameStarted && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        _startCanvas.SetActive(false); // Hide the Title and Play button
        Time.timeScale = 1f; // Resume the game
    }

    public void GameOver()
    {
        _gameOverCanvas.SetActive(true); // Show the Game Over canvas
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
