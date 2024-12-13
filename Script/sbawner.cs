using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class sbawner : MonoBehaviour
{
    public float queueTime = 1.5f;
    private float time = 0f;
    public GameObject obstacle;
    public float height;
    private bool gameStarted = false; // Tracks if the game has started

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted) // Stop spawning until the game starts
        {
            if (Input.GetMouseButtonDown(0)) // Check for the first click
            {
                gameStarted = true; // Enable spawning after the first click
                time = queueTime;
            }
            return;
        }

        // Spawn obstacles after the game starts
        time += Time.deltaTime;
        if (time > queueTime)
        {
            GameObject go = Instantiate(obstacle);
            go.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            time = 0;
            Destroy(go, 10);
        }
    }
}
