using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Cecil.Cil;

public class Ground : MonoBehaviour
{
    public float moveSpeed = 3f;  // Speed at which the ground moves
    public float resetPositionX = -10f;  // Position where the ground should reset after it moves off-screen
    public float startPositionX = 10f;  // The initial position of the ground (set based on your scene)

    private Vector3 startPosition;
    private bool isGameStarted = false;  // Track whether the game has started

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Wait for the first click to start the ground movement
        if (isGameStarted)
        {
            // Move the ground to the left
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            // If the ground moves out of the screen, reset its position
            if (transform.position.x <= resetPositionX)
            {
                ResetGroundPosition();
            }
        }

        // Start the game and the ground movement when the player clicks
        if (Input.GetMouseButtonDown(0) && !isGameStarted)
        {
            isGameStarted = true;
        }
    }

    // Reset the ground to its initial position to create the looping effect
    void ResetGroundPosition()
    {
        transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
    }
}
