using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Cecil.Cil;

public class CactusMovement : MonoBehaviour
{
    public float speed = 2f;
    public float maxHeight = 5f;

    private void Update()
    {
        // Make the cactus rise from the ground
        transform.position += Vector3.up * speed * Time.deltaTime;

        // Destroy the cactus when it reaches a certain height
        if (transform.position.y >= maxHeight)
        {
            Destroy(gameObject);
        }
    }
}
