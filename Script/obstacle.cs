using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public float speed; // Speed of pipes
    public static float globalSpeed; // Static variable to share speed globally

    void Start()
    {
        globalSpeed = speed; // Set initial global speed
    }

    void Update()
    {
        // Move the pipe left
        transform.position += ((Vector3.left * speed) * Time.deltaTime);

        // Keep globalSpeed updated in case speed changes dynamically
        globalSpeed = speed;
    }
}
