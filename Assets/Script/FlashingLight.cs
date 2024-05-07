using UnityEngine;
using UnityEngine.Events; // Required for UnityEvent

public class FlashingLight : MonoBehaviour
{/*
    public Light warningLight;
    public float flashDuration = 0.5f;
    private bool isFlashing = false;

    // Define an event to trigger flashing
    public UnityEvent onTrainApproaching;

    void Start()
    {
        if (warningLight == null)
            warningLight = GetComponent<Light>();

        // Subscribe to the event
        if (onTrainApproaching != null)
            onTrainApproaching.AddListener(StartFlashingWithDelay);
    }

    public void StartFlashingWithDelay()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            InvokeRepeating("ToggleLight", 1.0f, flashDuration); // Start flashing after a delay of 1 second
        }
    }

    public void StopFlashing()
    {
        isFlashing = false;
        CancelInvoke("ToggleLight");
        warningLight.enabled = false; // Turn off light when not flashing
    }

    void ToggleLight()
    {
        warningLight.enabled = !warningLight.enabled;
    }

    void OnDestroy()
    {
        // Clean up to avoid memory leaks
        if (onTrainApproaching != null)
            onTrainApproaching.RemoveListener(StartFlashingWithDelay);
    }*/
}
