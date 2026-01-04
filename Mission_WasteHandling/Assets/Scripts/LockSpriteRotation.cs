using UnityEngine;

public class LockSpriteRotation : MonoBehaviour 
{
    [SerializeField] Transform physicsTransform;
    [SerializeField] Renderer targetRenderer;
    [SerializeField] float hideDelay = 0.1f; // Time to wait before hiding
    [SerializeField] float startDelay = 1f; // Time before enabling functionality
    
    private Vector3 previousPosition;
    private float timeSinceLastMovement;
    private float timeSinceStart;
    private bool hasStarted = false;

    private bool isRinsed = false;

    void Start()
    {
        previousPosition = physicsTransform.position;
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        // Start with renderer disabled
        targetRenderer.enabled = false;
        timeSinceStart = 0f;
        
        isRinsed = false;
    }

    public void rinseMilk()
    {
        isRinsed = true;
        targetRenderer.enabled = false; // Ensure it's not visible when rinsed
    }

    void LateUpdate() 
    {
        // Handle startup delay
        if (!hasStarted)
        {
            timeSinceStart += Time.deltaTime;
            if (timeSinceStart >= startDelay)
            {
                hasStarted = true;
            }
            else
            {
                // Still in startup delay, just follow position but don't show
                transform.position = physicsTransform.position + new Vector3(1, 1, 0);
                previousPosition = physicsTransform.position;
                return;
            }
        }

        // Normal movement detection logic
        Vector3 displacement = physicsTransform.position - previousPosition;

        if (displacement.magnitude > 0.0001f && !isRinsed) // Considered as movement
        {
            timeSinceLastMovement = 0f;
            targetRenderer.enabled = true;
        }
        else
        {
            timeSinceLastMovement += Time.deltaTime;

            // Only hide after no movement for hideDelay seconds
            if (timeSinceLastMovement >= hideDelay)
            {
                targetRenderer.enabled = false;
            }
        }

        transform.position = physicsTransform.position + new Vector3(1.75f, 1.75f, 0);
        previousPosition = physicsTransform.position;
    }
}