using UnityEngine;
using UnityEngine.UI; // Needed if using UI Image

public class ThrobbingTitle : MonoBehaviour
{
    [Header("Throbbing Settings")]
    [SerializeField] private float minScale = 0.9f;    // Smallest size
    [SerializeField] private float maxScale = 1.1f;    // Largest size
    [SerializeField] private float speed = 2f;         // Pulses per second

    private RectTransform rectTransform; // For UI
    private Vector3 initialScale;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("ThrobbingTitle requires a RectTransform (UI Image).");
        }

        initialScale = rectTransform.localScale;
    }

    void Update()
    {
        if (rectTransform == null) return;

        // Sine wave oscillation between -1 and 1
        float scaleFactor = Mathf.Sin(Time.time * speed * Mathf.PI * 2);

        // Map sine from [-1,1] to [minScale,maxScale]
        float mappedScale = Mathf.Lerp(minScale, maxScale, (scaleFactor + 1f) / 2f);

        rectTransform.localScale = initialScale * mappedScale;
    }
}