using UnityEngine;
using Mono.Cecil.Cil;

public class ScoreCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreCanvas; // Reference to the score canvas
    private bool gameStarted = false; // Flag to track if the game has started

    // Start is called before the first frame update
    private void Start()
    {
        // Initially hide the score canvas
        scoreCanvas.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for the first click to start the game
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            // Show the score canvas after the first click
            scoreCanvas.SetActive(true);
        }
    }
}
