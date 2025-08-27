using System.Collections;
using UnityEngine;

public class flcker : MonoBehaviour
{
    public Light lightToToggle;   // Reference to the Light component
    public float interval = 1.0f; // Time interval in seconds

    private bool isLightOn = true;

    void Start()
    {
        if (lightToToggle == null)
        {
            lightToToggle = GetComponent<Light>();
        }

        // Start the toggling coroutine
        StartCoroutine(ToggleLight());
    }

    IEnumerator ToggleLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            isLightOn = !isLightOn;
            lightToToggle.enabled = isLightOn;
        }
    }
}
