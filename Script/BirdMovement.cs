using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public GameObject objectToSpawn; // The object to spawn (e.g., bird, cactus)
    public float spawnDelay = 1f; // Delay between spawns (set to 1 for more frequent spawns)
    public float spawnRangeY = 5f; // The Y-axis range for spawning objects (adjust as needed)
    public float moveSpeed = 5f; // Speed at which the object moves from right to left
    public float heightLimitMin = -3f; // Minimum Y position for spawning (height limiting)
    public float heightLimitMax = 3f;  // Maximum Y position for spawning (height limiting)

    private Camera mainCamera;
    private bool gameStarted = false; // Flag to check if the game has started
    private bool isBirdSpawned = false; // Flag to ensure only one bird is spawned at a time
    private float nextSpawnTime = 0f; // The time when the next bird should spawn

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Start the game when the player clicks for the first time
        if (!gameStarted && Input.GetMouseButtonDown(0)) // Check for first click
        {
            gameStarted = true; // Set game started flag to true
        }

        // Spawn birds more regularly after the game starts
        if (gameStarted && Time.time >= nextSpawnTime) // Check if it's time to spawn a new bird
        {
            nextSpawnTime = Time.time + spawnDelay; // Set the time for the next spawn
            SpawnObjects(); // Call the method to spawn the bird
        }
    }

    void SpawnObjects()
    {
        if (!isBirdSpawned) // Check if a bird is already spawned
        {
            // Calculate the spawn position off-screen on the right side of the camera
            float spawnX = mainCamera.transform.position.x + mainCamera.orthographicSize * 2; // Beyond the camera view on the right

            // Limit the spawn height within the specified range
            float spawnY = Random.Range(heightLimitMin, heightLimitMax); // Use height limits for Y position

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            // Instantiate the object at the calculated position
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            // Start the object movement from right to left
            spawnedObject.AddComponent<MoveObject>().Initialize(moveSpeed);

            isBirdSpawned = true; // Set the flag to true to prevent spawning another bird until the current one is destroyed
        }
    }

    public void BirdDestroyed()
    {
        isBirdSpawned = false; // Reset the flag when the bird is destroyed
    }
}

public class MoveObject : MonoBehaviour
{
    private float speed;

    public void Initialize(float moveSpeed)
    {
        speed = moveSpeed;
    }

    void Update()
    {
        // Move the object from right to left (relative to its own position)
        transform.position += Vector3.left * speed * Time.deltaTime;

        // If the object moves off-screen to the left, destroy it
        if (transform.position.x < Camera.main.transform.position.x - Camera.main.orthographicSize * 2)
        {
            Destroy(gameObject);

            // Notify the BirdMovement script that the bird has been destroyed
            FindFirstObjectByType<BirdMovement>()?.BirdDestroyed();
        }
    }
}
