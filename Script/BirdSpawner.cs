using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;  // Bird prefab to spawn
    public float spawnInterval = 3f; // Time interval between spawns
    public float spawnOffsetY = 1f; // Offset to avoid scoring area

    private float timer = 0f;
    private bool gameStarted = false; // Tracks if the game has started
    private float cameraVerticalExtent; // Camera boundary for spawn positions

    void Start()
    {
        cameraVerticalExtent = Camera.main.orthographicSize;
    }

    void Update()
    {
        // Game starts on first mouse click
        if (!gameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameStarted = true;
                timer = spawnInterval; // Start timer after the first click
            }
            return; // Do not spawn birds until game starts
        }

        // Wait for the first score to spawn birds
        int currentScore = Score.instance.GetScore();
        if (currentScore < 1)
        {
            return; // Don't spawn birds until at least 1 point is earned
        }

        // Bird spawning timer logic
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBird();
            timer = 0f; // Reset timer after spawning
        }
    }

    void SpawnBird()
    {
        // Random Y position within camera view avoiding scoring area
        float spawnY = Random.Range(-cameraVerticalExtent + spawnOffsetY, cameraVerticalExtent - spawnOffsetY);
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);

        // Instantiate the bird and give it leftward velocity
        GameObject bird = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bird.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Set bird speed to match the global pipe speed dynamically
            rb.linearVelocity = new Vector2(-obstacle.globalSpeed, 0);
        }

        // Destroy the bird after 10 seconds to avoid memory leaks
        Destroy(bird, 10f);
    }
}
