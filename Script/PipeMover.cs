using UnityEngine;

public class PipeMover : MonoBehaviour
{
    public float verticalSpeed = 0.2f; // Controlled vertical movement speed
    public bool isNightMode = false;   // Determines if vertical movement is active

    private float upperBound; // Upper vertical bound
    private float lowerBound; // Lower vertical bound
    private int direction = 1; // Direction of movement (1 = up, -1 = down)

    void Start()
    {
        // Calculate camera bounds for vertical movement
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize;
        float screenBottom = mainCamera.transform.position.y - cameraHeight;
        float screenTop = mainCamera.transform.position.y + cameraHeight;

        // Define movement boundaries
        upperBound = screenTop - 0.8f;
        lowerBound = screenBottom + 0.8f;
    }

    void Update()
    {
        // Only apply vertical movement when night mode is active
        if (isNightMode)
        {
            float movement = direction * verticalSpeed * Time.deltaTime;
            transform.position += new Vector3(0, movement, 0);

            // Reverse direction at boundaries
            if (transform.position.y >= upperBound)
            {
                transform.position = new Vector3(transform.position.x, upperBound, transform.position.z);
                direction = -1; // Move downward
            }
            else if (transform.position.y <= lowerBound)
            {
                transform.position = new Vector3(transform.position.x, lowerBound, transform.position.z);
                direction = 1; // Move upward
            }
        }
    }
}
