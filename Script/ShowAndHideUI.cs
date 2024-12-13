using System.Collections;
using UnityEngine;
using Mono.Cecil.Cil;

public class ShowAndHideUI : MonoBehaviour
{
    [SerializeField] private Canvas uiCanvas; // Reference to your canvas
    private bool gameStarted = false;

    void Start()
    {
        // Initially, make sure everything is visible
        ShowUI();
    }

    void Update()
    {
        // Listen for the first click
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            // Hide UI elements when the first click happens
            HideUI();
            gameStarted = true;
        }
    }

    // Function to show all UI elements under the Canvas
    void ShowUI()
    {
        foreach (Transform child in uiCanvas.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    // Function to hide all UI elements under the Canvas
    void HideUI()
    {
        foreach (Transform child in uiCanvas.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
