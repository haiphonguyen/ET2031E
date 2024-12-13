using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Cecil.Cil;

public class Blayer : MonoBehaviour
{
    private float velocity = 2.4f;
    private Rigidbody2D rb;
    private bool gameStarted = false; // Flag to track if the game has started
    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component
        rb.simulated = false; // Disable physics initially so the player doesn't move or fall
        animator.enabled = false; // Disable animation until the first click
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameStarted)
        {
            // Start the game when the player clicks for the first time
            if (Input.GetMouseButtonDown(0))
            {
                gameStarted = true;
                rb.simulated = true; // Enable physics
                animator.enabled = true; // Enable the animation after the game starts
                rb.linearVelocity = Vector2.up * velocity; // Make the character jump immediately
            }
        }
        else
        {
            // Allow player to fly after the game has started
            if (Input.GetMouseButtonDown(0))
            {
                rb.linearVelocity = Vector2.up * velocity; // Continue flying
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.GameOver();
    }
}
