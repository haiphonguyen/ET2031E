using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Cecil.Cil;

public class PipeSpawner : MonoBehaviour
{
    public float queueTime = 1.5f;
    private float time = 0f;
    public GameObject dayPipe; // Day pipe prefab
    public GameObject nightPipe; // Night pipe prefab
    public float height;
    private bool gameStarted = false; // Tracks if the game has started
    private bool isNight = false; // Tracks if the current state is night

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
            GameObject pipePrefab = isNight ? nightPipe : dayPipe; // Choose the correct pipe prefab
            GameObject go = Instantiate(pipePrefab);
            go.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            time = 0;
            Destroy(go, 10);
        }
    }

    public void SetNightMode(bool night)
    {
        isNight = night; // Update the state to switch pipe type
    }
}
