using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSbawner : MonoBehaviour
{
    public float queueTime = 1.5f;   // Time interval between spawns
    private float time = 0f;         // Timer to track spawn intervals
    public GameObject dayPipe;       // Day pipe prefab
    public GameObject nightPipe;     // Night pipe prefab
    public float height;             // Vertical spawn range
    private bool gameStarted = false; // Tracks if the game has started
    private bool isNight = false;    // Tracks if the current state is night
    private List<GameObject> spawnedPipes = new List<GameObject>(); // Track spawned pipes

    void Update()
    {
        if (!gameStarted) // Stop spawning until the game starts
        {
            if (Input.GetMouseButtonDown(0)) // Check for the first click
            {
                gameStarted = true; // Enable spawning after the first click
                time = queueTime;   // Reset the timer
            }
            return;
        }

        // Spawn obstacles after the game starts
        time += Time.deltaTime;
        if (time > queueTime)
        {
            GameObject pipePrefab = isNight ? nightPipe : dayPipe; // Choose the correct pipe prefab
            GameObject go = Instantiate(pipePrefab);
            go.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);

            // Attach the PipeMover script and set night mode
            PipeMover mover = go.AddComponent<PipeMover>();
            //mover.horizontalSpeed = 2.0f; // Set horizontal speed
            mover.isNightMode = isNight;  // Enable vertical movement only if in night mode

            spawnedPipes.Add(go); // Add spawned pipe to the list

            time = 0; // Reset spawn timer
            Destroy(go, 10); // Destroy the pipe after 10 seconds
        }
    }

    public void SetNightMode(bool night)
    {
        if (isNight != night)
        {
            // Destroy all currently spawned pipes before switching theme
            foreach (GameObject pipe in spawnedPipes)
            {
                Destroy(pipe);
            }
            spawnedPipes.Clear(); // Clear the list

            isNight = night; // Update the state to switch pipe type

            // Immediately start spawning pipes after theme change
            time = queueTime; // Reset the timer to avoid delay in spawning the first pipe
        }
    }
}
