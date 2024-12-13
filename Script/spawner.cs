using System.Collections;
using UnityEngine;
using Mono.Cecil.Cil;

public class spawner : MonoBehaviour
{
    public GameObject prefab; // Prefab to be spawned
    public float moveSpeed = 5f; // Speed of movement
    public float spawnDelay = 3f; // Delay between spawns (adjustable in the inspector)

    private Vector3 offScreenPosition; // Position where the object should start off-screen
    private Vector3 offScreenEndPosition; // Position where the object moves off-screen (past the camera)

    private bool gameStarted = false; // Flag to check if the game has started

    void Start()
    {
        // Get the camera's view center
        offScreenPosition = new Vector3(Camera.main.transform.position.x + Camera.main.orthographicSize * 2, Camera.main.transform.position.y, 0);
        offScreenEndPosition = new Vector3(Camera.main.transform.position.x - Camera.main.orthographicSize * 2, Camera.main.transform.position.y, 0);
    }

    void Update()
    {
        // Start the game when the player clicks for the first time
        if (!gameStarted && Input.GetMouseButtonDown(0)) // Check for first click
        {
            gameStarted = true; // Set game started flag to true
            StartCoroutine(SpawnPrefabs()); // Start spawning prefabs with delay
        }
    }

    // Coroutine to spawn prefabs with a delay
    private IEnumerator SpawnPrefabs()
    {
        while (gameStarted)
        {
            SpawnAndMovePrefab(); // Spawn and move the prefab
            yield return new WaitForSeconds(spawnDelay); // Wait for the specified delay before spawning the next prefab
        }
    }

    void SpawnAndMovePrefab()
    {
        // Instantiate the prefab at the right edge of the screen (off-screen)
        GameObject newPrefab = Instantiate(prefab, offScreenPosition, Quaternion.identity);

        // Start the movement past the camera
        StartCoroutine(MovePastCamera(newPrefab));
    }

    // Coroutine to move the prefab past the camera
    private IEnumerator MovePastCamera(GameObject prefab)
    {
        // Move the prefab from right to left, past the camera's view
        while (prefab.transform.position.x > offScreenEndPosition.x)
        {
            // Move the prefab towards the left, beyond the camera's left side
            prefab.transform.position = Vector3.MoveTowards(prefab.transform.position, offScreenEndPosition, moveSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }

        // After the object has passed the camera, destroy it
        Destroy(prefab);
    }
}
