using System.Collections;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public SpriteRenderer dayBackground;  // The day background sprite renderer
    public SpriteRenderer nightBackground; // The night background sprite renderer
    public PipeSbawner pipeSpawner; // Reference to the PipeSpawner
    public int transitionScore = 15; // Score threshold for switching background

    private bool isNight = false; // Tracks if the current background is night
    private int nextTransitionScore; // Tracks the score at which the next transition occurs

    private void Start()
    {
        // Ensure the initial state
        nextTransitionScore = transitionScore;
        SetBackgroundState(false); // Start with the day background
    }

    private void Update()
    {
        // Continuously monitor the current score
        int currentScore = Score.instance.GetScore();

        // Check if the score has reached the threshold for background transition
        if (currentScore >= nextTransitionScore)
        {
            // Toggle between day and night
            isNight = !isNight;

            // Smooth transition between backgrounds
            StartCoroutine(TransitionBackground(isNight));

            // Notify the PipeSpawner
            pipeSpawner.SetNightMode(isNight);

            // Update the score threshold for the next transition
            nextTransitionScore += transitionScore;
        }
    }

    private IEnumerator TransitionBackground(bool toNight)
    {
        float duration = 1.0f; // Duration of the transition
        float elapsed = 0.0f;

        SpriteRenderer fromBackground = toNight ? dayBackground : nightBackground;
        SpriteRenderer toBackground = toNight ? nightBackground : dayBackground;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = elapsed / duration;

            // Adjust alpha values for a smooth transition
            fromBackground.color = new Color(1, 1, 1, 1 - alpha);
            toBackground.color = new Color(1, 1, 1, alpha);

            yield return null;
        }

        // Ensure the final state after the transition
        fromBackground.color = new Color(1, 1, 1, 0); // Fully transparent
        toBackground.color = new Color(1, 1, 1, 1); // Fully visible
    }

    private void SetBackgroundState(bool toNight)
    {
        dayBackground.color = toNight ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
        nightBackground.color = toNight ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
    }

}